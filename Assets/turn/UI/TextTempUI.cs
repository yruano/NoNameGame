using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTempUI : MonoBehaviour
{
    [SerializeField]
    public TMP_Text output;  // TextMeshPro ��ü����
    public TMP_Text battleLog;  // TextMeshPro ��ü����
    public InputField input;

    //����� ��ų ������
    public string skillData = "";
    //����� ���� ������
    public string monsterData = "";
    //�Է� �߰� �ޱ����� ����
    private bool inputReceived = false;
    //�Է¹��� ���ڿ�
    private string inputText = "";
    public void Awake()
    {
        //Text����
        output = GameObject.Find("Text").GetComponent<TMP_Text>();
        battleLog = GameObject.Find("batteLog").GetComponent<TMP_Text>();
        //inputField ����
        input = GameObject.Find("InputField").GetComponent<InputField>();
        //�̺�Ʈ ������ ���
        input.onEndEdit.AddListener(OnInputEndEdit);
        //�α� �ʱ�ȭ
        battleLog.text = "";
    }

    //�Է� �߰�
    //�Է� �Ϸ�� �ش� ���ڿ� ����
    private void OnInputEndEdit(string input)
    {
        inputReceived = true;
        inputText = input;
    }
    //�Է� �����ϸ� �����
    public System.Collections.IEnumerator WaitForInput()
    {
        inputReceived = false;
        input.ActivateInputField();

        while (!inputReceived)
        {
            yield return null;
        }
    }

    //���� ��Ʈ�ѷ����Լ� ��ų�迭�� ���� ��ųʸ��� �ް�, �װ� �÷��̾�� �о���
    public void DisplayToPlayer(Dictionary<int, TurnMonster> BMonsters, List<List<double>> BSkillQueue)
    {
        skillData += "skill infor\n";
        //��ų �����̶� ����, �켱���� �о���
        foreach(var skill in BSkillQueue)
        {
            skillData += "monster ID: " + (int)skill[skill.Count - 1] + " \nusing: ";
            //����
            if ((int)skill[Skill.ID] % 10 == 1)
            {
                //������ ���
                skillData += "Attack \ndamage: " + skill[SkillAttack.DAMAGE] + "\n";
            }

            //��
            else if ((int)skill[Skill.ID] % 10 == 4)
            {
                //���, ġ�ᷮ ���
                skillData += "heal \ntarget ID: " + skill[0] + ", amount: " + skill[SkillHeal.AMOUNT] + "\n";
            }

            //����
            else if ((int)skill[Skill.ID] % 10 == 2)
            {
                //��� ��ü, ��� ����, ���갪 ����, ���갪 ����, ���ӽð� ���
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

            //�����
            else if ((int)skill[Skill.ID] % 10 == 3)
            {
                //��� ��ü, ��� ����, ���갪 ����, ���갪 ����, ���ӽð� ���
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

            //�켱���� ���
            skillData += "priority: " + skill[Skill.PRIORITY] + "\n";


            //�ٳѱ�
            skillData += "\n";
        }

        monsterData += "alived montser infor\n";
        //����ִ� ���� �о���
        foreach (var monster in BMonsters)
        {
            monsterData += "ID: " + monster.Key + "\n";
            monsterData += "HP: " + monster.Value.MOriginStat[TurnMonster.NOWHP] + "/" + monster.Value.MProcessedStat()[TurnMonster.MAXHP] + "\n";

        }

        //���� ���ڿ� ���
        setText(skillData + "\n\n" + monsterData);
        //��¹� �ʱ�ȭ
        skillData = "";
        monsterData = "";
    }

    // TextMeshPro ��ü�� �ؽ�Ʈ�� ������.
    public void setText(string text)
    {
        output.text = text;
    }
    public string GetInputText()
    {
        return inputText;
    }
    //log�̴� �ؽ�Ʈ ������
    public void setLogText(string text) 
    {
        battleLog.text += text;

    }
}