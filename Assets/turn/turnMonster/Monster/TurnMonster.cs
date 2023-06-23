//on turn system, in battle, contain basic monter infor 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//���� �ý��ۿ��� ������ ����� ������ �⺻������

public class TurnMonster : MonoBehaviour
{
    //�����غ� == �ൿ ��������
    public bool MReady = true;
    //�ε����� �迭��� 
    public List<double> MOriginStat = new List<double>{ 100.0, 100.0, 1.0, 1.0, 0.0, 0.0 }; //���� ����
    public const int MAXHP = 0;
    public const int NOWHP = 1;
    public const int DAMAGE = 2;
    public const int ARMOR = 3;
    public const int SPEED = 4;
    /*//���� �������
    public double MMaxHp = 100.0; 
    public double MNowHp = 100.0;
    public double MDamage = 1.0;
    public double MArmor = 0.0;
    public double MSpeed = 1.0;
    //public double MRange = 1.0; ���ӿ� �Ÿ������� ���� ����
    //public double MPriority = 1.0;//�켱����?
    //public double luck = 1.0; Ȯ�� ���� ��ġ(���� �ش� ���� ����)*/

    //������Ʈ�ѷ� ����
    public BuffController buffController;

    [SerializeField]
    //���� �ִ� ��ų���� ����. 
    public List<Skill> MGotSkill = new List<Skill>();
    //�켱���� ���� [0]�ּ� [1]�ִ�, ���ڰ� Ŭ���� ���� �켱����
    public int[] priority = new int[2];

    //������
    public void Awake()
    {
        //���� ��Ʈ�Ѱ� ����
        buffController = GetComponent<BuffController>();
    }

   //�Է� ����
    //���� ��Ʈ�ѷ��� ���� ����μ� ����(�迭����)
    public void MSetRegistBuff(List<double> newBuff)
    {
        buffController.FAddBuff(newBuff);
    }
    
    //���� ��Ʈ�ѷ��� ���� ����μ� ����(���� �μ� ����)
    public void MSetRegistBuff(double AddVal, double MultiVal, double Target, double left, double Id)
    {
        buffController.FAddBuff(AddVal, MultiVal, Target, left, Id);
    }
    
    //�Է� ���� �� ���� ü�¿� ����
    public void MSetHealHP(double val)
    {
        MOriginStat[NOWHP] += val;
    }
    
    //�Է¹��� ������ ���� ���� ��ŭ�� �����ϰ� ü�°��� ������
    public void MSetDamagedHP(double val)
    {
        MOriginStat[NOWHP] -= val - buffController.ProcessedStat[ARMOR];
    }
    
    //���� �Ѿ�� ȣ��� �� ȣ����
    public void MNextTurn()
    {
        buffController.FNextTurn();
    }

   //��� ����
    //���� ���� ���� ��ȯ
    public List<double> MProcessedStat()
    {
        return buffController.ProcessedStat;
    }

    //����� ��ų�� ��갪�� �켱���� ��ü�� ��ȯ
    public List<double> MgetRandomSkill()
    {
        //���� ��ų�� �ϳ��� �������� ���
        List <double> tmpSkill = MGotSkill[Random.Range(0, MGotSkill.Count)].SFunction();
        //�����ִ� �켱���� ������ �ٿ�
        tmpSkill[Skill.PRIORITY] = Random.Range(priority[0], priority[1]);
        //��ȯ
        return tmpSkill;
    }
    
    //���� ü�� ��ȯ
    public double MGetLeftHP()
    {
        return MOriginStat[NOWHP];
    }
}
