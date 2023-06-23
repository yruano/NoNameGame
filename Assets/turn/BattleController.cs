using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
//전투컨트롤러,
public class BattleController : MonoBehaviour
{
    [SerializeField]
    //전투 턴
    int Bturn = 0;
    //참여하는 몬스터들, 0~양수를 키로 가짐
    public Dictionary<int, TurnMonster> BMonsters = new Dictionary<int, TurnMonster>();
    //이번턴에 제출된 스킬의 연산값
    List<List<double>> BSkillQueue = new List<List<double>>();

    //임시UI랑 연결용
    public TextTempUI ui;

    public void Awake()
    {
        //임시 ui담당이랑 연결
        ui = GetComponent<TextTempUI>();
    }

    //받을 정보 다 받고 턴 시작할때 호출 
    public void turnStart()
    {
        //전투 턴 초기화
        Bturn = 0;
        //메인 턴 루틴 시작
        StartCoroutine(turnMainRoutine());
    }
    private IEnumerator turnMainRoutine()
    {

        //몬스터 전부 사망시 전투 종료
        while (BMonsters.Count > 0)
        {
            //0단계, 스킬 리스트 비우고 턴 넘기며 호출할거 호출함
            BSkillQueue.Clear();
            Bturn++;
            foreach (var oneMonster in BMonsters)
                oneMonster.Value.MNextTurn();

            //1단계, 몬스터에게 스킬 정보 가져옴
            fSubmitSkillFromMonster();

            //2단계, 플레이어에게 사용할 스킬/방해할 스킬 입력받음(임시용)------
            //일단 UI담당 스크립트에게 정보를 넘김, 여기 있는건 임시용임
            ui.DisplayToPlayer(BMonsters, BSkillQueue);

            // 입력 대기
            yield return StartCoroutine(ui.WaitForInput());
            tmpInputProcess();

            //우선순위 기준 정렬
            SortQueue();

            //3단계, 리스트에 남아있는 스킬의 결과 연산
            fProcessSkills();

        }
    }

    //몬스터에게 스킬 정보 가져와 스킬 리스트 갱신
    public void fSubmitSkillFromMonster()
    {
        foreach (var oneMonster in BMonsters)
        {
            //해당 몬스터가 행동 불가라면
            if (!oneMonster.Value.MReady)
                //해당 몬스터는 행동 스킵
                continue;

            //해당 몬스터에게 등록된 스킬중 랜덤한 하나의 스킬의 연산값 받아옴
            List<double> tmp = oneMonster.Value.MgetRandomSkill();
            //누구에게 갈건지 타입으로 확인. 짝수면 몬스터, 홀수면 플레이어
            //짝수라면
            if (tmp[Skill.TYPE] % 2 == 0)
                //타입을 스킬의 대상 몬스터ID로 변경
                tmp[0] = BMonsters.Keys.ElementAt(Random.Range(0, BMonsters.Count));
            //홀수라면
            else
                //타입-> 플레이어 대상으로 변경, 현재 플레이어 대상은 구상안되어있으므로 일단 -1
                tmp[0] = -1;

            //맨 뒤에 스킬 누가 썼는지 내부 ID로 기록
            tmp.Add(oneMonster.Key);

            //스킬 대기리스트에 저장
            BSkillQueue.Add(tmp);
        }
    }

   //리스트에 있는 스킬의 결과 연산
    public void fProcessSkills()
    {
        //           + 가장 뒤는 스킬 쓴 대상
        foreach (var oneSkill in BSkillQueue)
        {
            //몬스터 대상이고
            if (oneSkill[0] >= 0)
            {
                //스킬을 계산하면 안될 때 조건

                    //사용자가 플레이어가 아닌데
                if (!((int)oneSkill[oneSkill.Count - 1] <= -1) &&
                     //대상 몬스터가 존재하지 않거나,
                    (!BMonsters.ContainsKey((int)oneSkill[0]) ||
                     //스킬을 사용한 몬스터가 존재하지 않거나
                     !BMonsters.ContainsKey((int)oneSkill[oneSkill.Count - 1]) ||
                     //스킬을 사용한 몬스터가 계산중인 시점에서 행동 불가능하다면
                     !BMonsters[(int)oneSkill[oneSkill.Count - 1]].MReady) )
                    //해당 스킬은 스킵
                    continue;

                //공격이라면
                if ((int)oneSkill[Skill.ID] % 10 == 1)
                {
                    //다단히트가 아니라면 == 10의 자리수가 9가 아니라면
                    if (((int)oneSkill[Skill.ID] % 100) / 10 != 9)
                    {
                        //사용자가 플레이어라면
                        if ((int)oneSkill[oneSkill.Count - 1] < 0)
                            //대상 몬스터 현재 체력에 연산값 전달
                            BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE]);
                        //사용자가 몬스터라면
                        else
                            //대상 몬스터 현재 체력에 연산값 + 몬스터의 데미지 추가값을 더함
                            BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE] + BMonsters[(int)oneSkill[oneSkill.Count - 1]].MProcessedStat()[TurnMonster.DAMAGE]);
                    }
                    //다단히트라면
                    else
                    {
                        //사용자가 플레이어라면
                        if ((int)oneSkill[oneSkill.Count - 1] < 0)
                            //다단 히트 수 만큼
                            for(int i = 0; i < (int)oneSkill[SkillAttackMultiHit.HITCOUNT]; i++)
                                //대상 몬스터 현재 체력에 연산값 전달
                                BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE]);
                        //사용자가 몬스터라면
                        else
                            //다단 히트 수 만큼
                            for (int i = 0; i < (int)oneSkill[SkillAttackMultiHit.HITCOUNT]; i++)
                                //대상 몬스터 현재 체력에 연산값 + 몬스터의 데미지 추가값을 더함
                                BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE] + BMonsters[(int)oneSkill[oneSkill.Count - 1]].MProcessedStat()[TurnMonster.DAMAGE]);
                    }

                    //피격자가 남은 체력이 없다면
                    if (BMonsters[(int)oneSkill[0]].MGetLeftHP() <= 0)
                    {
                        //해당 몬스터 파괴
                        Destroy(BMonsters[(int)oneSkill[0]].gameObject);
                        BMonsters.Remove((int)oneSkill[0]);
                    }
                }

                //힐이라면
                else if ((int)oneSkill[Skill.ID] % 10 == 4)
                    //대상 몬스터 현재 체력에 연산값 전달
                    BMonsters[(int)oneSkill[0]].MSetHealHP(oneSkill[SkillHeal.AMOUNT]);

                //버프||디버프라면
                else if ((int)oneSkill[Skill.ID] % 10 == 2 || (int)oneSkill[Skill.ID] % 10 == 2)
                    //대상 몬스터 버프컨트롤러에 연산값 전달
                    BMonsters[(int)oneSkill[0]].buffController.FAddBuff(oneSkill[SkillBuff.ADD], oneSkill[SkillBuff.MULTI],
                                                                        oneSkill[SkillBuff.TARGETSTAT], oneSkill[SkillBuff.DURATION], oneSkill[Skill.ID]);
                //지속 데미지라면(미구현)------
            }
            //플레이어 대상이고(미구현)------
            //else;
        }
    }

    //우선순위 기준으로 BSkillQueue를 정렬, 내림차순 정렬
    public void SortQueue()
    {
        BSkillQueue.Sort((x, y) => y[Skill.PRIORITY].CompareTo(x[Skill.PRIORITY])) ;
    }

    //!!임시!! 플레이어의 입력을 받아와 계산
    private void tmpInputProcess()
    {
        //입력 받아옴
        string tmp = ui.GetInputText();
        //공격이라면
        if (tmp[0] == 'A' || tmp[0] == 'a')
        {
            //입력받은 키가 있다면
            if(BMonsters.ContainsKey(tmp[1] - '0'))
            {
                //스킬 큐에 입력
                List<double> tmpskill = new List<double>
                {
                    //대상      , id(공격), 우선순위(임의로 1), 데미지(임의로 25), 사용자(플레이어)
                    tmp[1] - '0', 1       , 1                 , 25               , -1
                };
                BSkillQueue.Add(tmpskill);
            }
        }
    }
}