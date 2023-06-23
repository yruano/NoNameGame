using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
//������Ʈ�ѷ�,
public class BattleController : MonoBehaviour
{
    [SerializeField]
    //���� ��
    int Bturn = 0;
    //�����ϴ� ���͵�, 0~����� Ű�� ����
    public Dictionary<int, TurnMonster> BMonsters = new Dictionary<int, TurnMonster>();
    //�̹��Ͽ� ����� ��ų�� ���갪
    List<List<double>> BSkillQueue = new List<List<double>>();

    //�ӽ�UI�� �����
    public TextTempUI ui;

    public void Awake()
    {
        //�ӽ� ui����̶� ����
        ui = GetComponent<TextTempUI>();
    }

    //���� ���� �� �ް� �� �����Ҷ� ȣ�� 
    public void turnStart()
    {
        //���� �� �ʱ�ȭ
        Bturn = 0;
        //���� �� ��ƾ ����
        StartCoroutine(turnMainRoutine());
    }
    private IEnumerator turnMainRoutine()
    {

        //���� ���� ����� ���� ����
        while (BMonsters.Count > 0)
        {
            //0�ܰ�, ��ų ����Ʈ ���� �� �ѱ�� ȣ���Ұ� ȣ����
            BSkillQueue.Clear();
            Bturn++;
            foreach (var oneMonster in BMonsters)
                oneMonster.Value.MNextTurn();

            //1�ܰ�, ���Ϳ��� ��ų ���� ������
            fSubmitSkillFromMonster();

            //2�ܰ�, �÷��̾�� ����� ��ų/������ ��ų �Է¹���(�ӽÿ�)------
            //�ϴ� UI��� ��ũ��Ʈ���� ������ �ѱ�, ���� �ִ°� �ӽÿ���
            ui.DisplayToPlayer(BMonsters, BSkillQueue);

            // �Է� ���
            yield return StartCoroutine(ui.WaitForInput());
            tmpInputProcess();

            //�켱���� ���� ����
            SortQueue();

            //3�ܰ�, ����Ʈ�� �����ִ� ��ų�� ��� ����
            fProcessSkills();

        }
    }

    //���Ϳ��� ��ų ���� ������ ��ų ����Ʈ ����
    public void fSubmitSkillFromMonster()
    {
        foreach (var oneMonster in BMonsters)
        {
            //�ش� ���Ͱ� �ൿ �Ұ����
            if (!oneMonster.Value.MReady)
                //�ش� ���ʹ� �ൿ ��ŵ
                continue;

            //�ش� ���Ϳ��� ��ϵ� ��ų�� ������ �ϳ��� ��ų�� ���갪 �޾ƿ�
            List<double> tmp = oneMonster.Value.MgetRandomSkill();
            //�������� ������ Ÿ������ Ȯ��. ¦���� ����, Ȧ���� �÷��̾�
            //¦�����
            if (tmp[Skill.TYPE] % 2 == 0)
                //Ÿ���� ��ų�� ��� ����ID�� ����
                tmp[0] = BMonsters.Keys.ElementAt(Random.Range(0, BMonsters.Count));
            //Ȧ�����
            else
                //Ÿ��-> �÷��̾� ������� ����, ���� �÷��̾� ����� ����ȵǾ������Ƿ� �ϴ� -1
                tmp[0] = -1;

            //�� �ڿ� ��ų ���� ����� ���� ID�� ���
            tmp.Add(oneMonster.Key);

            //��ų ��⸮��Ʈ�� ����
            BSkillQueue.Add(tmp);
        }
    }

   //����Ʈ�� �ִ� ��ų�� ��� ����
    public void fProcessSkills()
    {
        //           + ���� �ڴ� ��ų �� ���
        foreach (var oneSkill in BSkillQueue)
        {
            //���� ����̰�
            if (oneSkill[0] >= 0)
            {
                //��ų�� ����ϸ� �ȵ� �� ����

                    //����ڰ� �÷��̾ �ƴѵ�
                if (!((int)oneSkill[oneSkill.Count - 1] <= -1) &&
                     //��� ���Ͱ� �������� �ʰų�,
                    (!BMonsters.ContainsKey((int)oneSkill[0]) ||
                     //��ų�� ����� ���Ͱ� �������� �ʰų�
                     !BMonsters.ContainsKey((int)oneSkill[oneSkill.Count - 1]) ||
                     //��ų�� ����� ���Ͱ� ������� �������� �ൿ �Ұ����ϴٸ�
                     !BMonsters[(int)oneSkill[oneSkill.Count - 1]].MReady) )
                    //�ش� ��ų�� ��ŵ
                    continue;

                //�����̶��
                if ((int)oneSkill[Skill.ID] % 10 == 1)
                {
                    //�ٴ���Ʈ�� �ƴ϶�� == 10�� �ڸ����� 9�� �ƴ϶��
                    if (((int)oneSkill[Skill.ID] % 100) / 10 != 9)
                    {
                        //����ڰ� �÷��̾���
                        if ((int)oneSkill[oneSkill.Count - 1] < 0)
                            //��� ���� ���� ü�¿� ���갪 ����
                            BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE]);
                        //����ڰ� ���Ͷ��
                        else
                            //��� ���� ���� ü�¿� ���갪 + ������ ������ �߰����� ����
                            BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE] + BMonsters[(int)oneSkill[oneSkill.Count - 1]].MProcessedStat()[TurnMonster.DAMAGE]);
                    }
                    //�ٴ���Ʈ���
                    else
                    {
                        //����ڰ� �÷��̾���
                        if ((int)oneSkill[oneSkill.Count - 1] < 0)
                            //�ٴ� ��Ʈ �� ��ŭ
                            for(int i = 0; i < (int)oneSkill[SkillAttackMultiHit.HITCOUNT]; i++)
                                //��� ���� ���� ü�¿� ���갪 ����
                                BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE]);
                        //����ڰ� ���Ͷ��
                        else
                            //�ٴ� ��Ʈ �� ��ŭ
                            for (int i = 0; i < (int)oneSkill[SkillAttackMultiHit.HITCOUNT]; i++)
                                //��� ���� ���� ü�¿� ���갪 + ������ ������ �߰����� ����
                                BMonsters[(int)oneSkill[0]].MSetDamagedHP(oneSkill[SkillAttack.DAMAGE] + BMonsters[(int)oneSkill[oneSkill.Count - 1]].MProcessedStat()[TurnMonster.DAMAGE]);
                    }

                    //�ǰ��ڰ� ���� ü���� ���ٸ�
                    if (BMonsters[(int)oneSkill[0]].MGetLeftHP() <= 0)
                    {
                        //�ش� ���� �ı�
                        Destroy(BMonsters[(int)oneSkill[0]].gameObject);
                        BMonsters.Remove((int)oneSkill[0]);
                    }
                }

                //���̶��
                else if ((int)oneSkill[Skill.ID] % 10 == 4)
                    //��� ���� ���� ü�¿� ���갪 ����
                    BMonsters[(int)oneSkill[0]].MSetHealHP(oneSkill[SkillHeal.AMOUNT]);

                //����||��������
                else if ((int)oneSkill[Skill.ID] % 10 == 2 || (int)oneSkill[Skill.ID] % 10 == 2)
                    //��� ���� ������Ʈ�ѷ��� ���갪 ����
                    BMonsters[(int)oneSkill[0]].buffController.FAddBuff(oneSkill[SkillBuff.ADD], oneSkill[SkillBuff.MULTI],
                                                                        oneSkill[SkillBuff.TARGETSTAT], oneSkill[SkillBuff.DURATION], oneSkill[Skill.ID]);
                //���� ���������(�̱���)------
            }
            //�÷��̾� ����̰�(�̱���)------
            //else;
        }
    }

    //�켱���� �������� BSkillQueue�� ����, �������� ����
    public void SortQueue()
    {
        BSkillQueue.Sort((x, y) => y[Skill.PRIORITY].CompareTo(x[Skill.PRIORITY])) ;
    }

    //!!�ӽ�!! �÷��̾��� �Է��� �޾ƿ� ���
    private void tmpInputProcess()
    {
        //�Է� �޾ƿ�
        string tmp = ui.GetInputText();
        //�����̶��
        if (tmp[0] == 'A' || tmp[0] == 'a')
        {
            //�Է¹��� Ű�� �ִٸ�
            if(BMonsters.ContainsKey(tmp[1] - '0'))
            {
                //��ų ť�� �Է�
                List<double> tmpskill = new List<double>
                {
                    //���      , id(����), �켱����(���Ƿ� 1), ������(���Ƿ� 25), �����(�÷��̾�)
                    tmp[1] - '0', 1       , 1                 , 25               , -1
                };
                BSkillQueue.Add(tmpskill);
            }
        }
    }
}