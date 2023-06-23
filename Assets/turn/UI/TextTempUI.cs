using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTempUI : MonoBehaviour
{
    [SerializeField]
    public TMP_Text output;  // TextMeshPro 객체추적
    public TMP_Text battleLog;  // TextMeshPro 객체추적
    public InputField input;

    //출력할 스킬 데이터
    public string skillData = "";
    //출력할 몬스터 데이터
    public string monsterData = "";
    //입력 중계 받기위한 변수
    private bool inputReceived = false;
    //입력받은 문자열
    private string inputText = "";
    public void Awake()
    {
        //Text연결
        output = GameObject.Find("Text").GetComponent<TMP_Text>();
        battleLog = GameObject.Find("batteLog").GetComponent<TMP_Text>();
        //inputField 연결
        input = GameObject.Find("InputField").GetComponent<InputField>();
        //이벤트 리스너 등록
        input.onEndEdit.AddListener(OnInputEndEdit);
        //로그 초기화
        battleLog.text = "";
    }

    //입력 중계
    //입력 완료시 해당 문자열 저장
    private void OnInputEndEdit(string input)
    {
        inputReceived = true;
        inputText = input;
    }
    //입력 감지하며 대기함
    public System.Collections.IEnumerator WaitForInput()
    {
        inputReceived = false;
        input.ActivateInputField();

        while (!inputReceived)
        {
            yield return null;
        }
    }

    //전투 컨트롤러에게서 스킬배열과 몬스터 딕셔너리를 받고, 그걸 플레이어에게 읽어줌
    public void DisplayToPlayer(Dictionary<int, TurnMonster> BMonsters, List<List<double>> BSkillQueue)
    {
        skillData += "skill infor\n";
        //스킬 쓴놈이랑 내용, 우선순위 읽어줌
        foreach(var skill in BSkillQueue)
        {
            skillData += "monster ID: " + (int)skill[skill.Count - 1] + " \nusing: ";
            //공격
            if ((int)skill[Skill.ID] % 10 == 1)
            {
                //데미지 출력
                skillData += "Attack \ndamage: " + skill[SkillAttack.DAMAGE] + "\n";
            }

            //힐
            else if ((int)skill[Skill.ID] % 10 == 4)
            {
                //대상, 치료량 출력
                skillData += "heal \ntarget ID: " + skill[0] + ", amount: " + skill[SkillHeal.AMOUNT] + "\n";
            }

            //버프
            else if ((int)skill[Skill.ID] % 10 == 2)
            {
                //대상 객체, 대상 스텟, 연산값 덧셈, 연산값 곱셈, 지속시간 출력
                skillData += "buff \ntarget ID: " + skill[0] + " \ntarget stat: ";
                if (skill[SkillBuff.TARGETSTAT] == TurnMonster.MAXHP) skillData += "max hp";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.NOWHP) skillData += "current hp";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.DAMAGE) skillData += "damage";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.ARMOR) skillData += "armor";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.SPEED) skillData += "speed";
                skillData += "\n applied ";
                if (skill[SkillBuff.ADD] != 0) skillData += " + " + skill[SkillBuff.ADD];
                if (skill[SkillBuff.ADD] != 0 && skill[SkillBuff.MULTI] != 1.0) skillData += " ,and ";
                if (skill[SkillBuff.MULTI] != 1.0) skillData += " * " + skill[SkillBuff.ADD];
                skillData += "\nduration turn: " + skill[SkillBuff.DURATION] + "\n";
            }

            //디버프
            else if ((int)skill[Skill.ID] % 10 == 3)
            {
                //대상 객체, 대상 스텟, 연산값 덧셈, 연산값 곱셈, 지속시간 출력
                skillData += "debuff \ntarget: player, \ntarget stat: ";
                if (skill[SkillBuff.TARGETSTAT] == TurnMonster.MAXHP) skillData += "max hp";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.NOWHP) skillData += "current hp";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.DAMAGE) skillData += "damage";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.ARMOR) skillData += "armor";
                else if (skill[SkillBuff.TARGETSTAT] == TurnMonster.SPEED) skillData += "speed";
                skillData += "\n applied to ";
                if (skill[SkillBuff.ADD] != 0) skillData += " + " + skill[SkillBuff.ADD];
                if (skill[SkillBuff.ADD] != 0 && skill[SkillBuff.MULTI] != 1.0) skillData += " ,and ";
                if (skill[SkillBuff.MULTI] != 1.0) skillData += " * " + skill[SkillBuff.ADD];
                skillData += "\nduration turn: " + skill[SkillBuff.DURATION] + "\n";
            }

            //우선순위 출력
            skillData += "priority: " + skill[Skill.PRIORITY] + "\n";


            //줄넘김
            skillData += "\n";
        }

        monsterData += "alived montser infor\n";
        //살아있는 몬스터 읽어줌
        foreach (var monster in BMonsters)
        {
            monsterData += "ID: " + monster.Key + "\n";
            monsterData += "HP: " + monster.Value.MOriginStat[TurnMonster.NOWHP] + "/" + monster.Value.MProcessedStat()[TurnMonster.MAXHP] + "\n";

        }

        //만든 문자열 출력
        setText(skillData + "\n\n" + monsterData);
        //출력문 초기화
        skillData = "";
        monsterData = "";
    }

    // TextMeshPro 객체의 텍스트를 변경함.
    public void setText(string text)
    {
        output.text = text;
    }
    public string GetInputText()
    {
        return inputText;
    }
    //log뽑는 텍스트 수정함
    public void setLogText(string text) 
    {
        battleLog.text += text;

    }
}