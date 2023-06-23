using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//� ������ �ɷ��ִ���, � ����� �Ǿ��ִ���, ��������� ������ ���Ҵ��� ���.
//������ �׻� (�⺻�� + ������) * ���������� ����,
public class BuffController : MonoBehaviour
{
    [SerializeField]
    private TurnMonster target;

    //[ID][] [0]������ [1]������ [2]��� ����Ǵ°� (-> TurnMonster.����� ���ǵǾ�����) [3]���� ����ð�
    public Dictionary<int, List<double>> BuffInfor = new Dictionary<int, List<double>>();
    public const int BuffAdd = 0;
    public const int BuffMulty = 1;
    public const int BuffTarget = 2;
    public const int BuffLeft = 3;
    public const int BuffId = 4;
    //���� ���� �� ��
    public List<double> ProcessedStat;
    //���� ���� ���� ������ �� [0]���� [1]����
    public List<List<double>> AttatchedRawValue = new List<List<double>>();

    //[0]ID [1]������ [2]���� ���ӽð�
    public List<List<double>> DotInfor = new List<List<double>>();//���ӵ����� ����
    public const int Dotid = 0;
    public const int DotDamage = 1;
    public const int DotLeft = 2;
    public double DotValue;//���ӵ������� ���ҵ� ü�� ����

    public void Awake()
    {
        //��� ��� ����
        target = GetComponent<TurnMonster>();
        //���� �� ���� �ʱ�ȭ
        ProcessedStat = new List<double>(target.MOriginStat);
        //���� �� ���� �ʱ�ȭ
        for (int i = 0; i < ProcessedStat.Count; i++)
        {
            AttatchedRawValue.Add(new List<double> { 0.0, 1.0 });
        }
    }

   //�ɸ� ������ ��ȯ
    public int Count()
    {
        return ProcessedStat.Count;
    }

   //�� ���� �Ѿ �� �ʿ��� ����
    //���� ����ð� ���� -1�ؼ� ���� �ϳ� �Ѿ�� ����
    public void FNextTurn()
    {
        //��������
        for (int i = 0; i < BuffInfor.Count; i++)
        {
            int key = BuffInfor.Keys.ElementAt(i);
            //�������� 0�̸� ����
            if (--BuffInfor[key][3] <= 0)
            {
                //���� ���� �� ���� ���� (���� + ������) * ������ 
                AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][BuffAdd] -= BuffInfor[key][BuffAdd];
                AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][BuffMulty] /= BuffInfor[key][BuffMulty];
                //���� ���� �� ���� ����
                ProcessedStat[ (int)BuffInfor[key][BuffTarget] ] = (target.MOriginStat[BuffTarget] + AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][0]) * AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][1];
                //��Ͽ��� ����
                BuffInfor.Remove(key);
                i--; //���������� �ε��� ��ġ ����
                //���� ������ ���ٸ� ���������� ��ü, �Ҽ������̶� ���� ���� �� �ִ°� �ٷ�����ֱ�����
                if(BuffInfor.Count <= 0)
                    ProcessedStat = target.MOriginStat;
            }
        }
        //���ӵ����� ����
        for(int i = 0; i < DotInfor.Count; i++)
        {
            //���� ���� 0�̸� ����
            if (--DotInfor[i][DotLeft] <= 0)
            {
                //����� �� ����
                DotValue -= DotInfor[i][DotDamage];
                //�迭���� ����
                DotInfor.RemoveAt(i);
                i--;
            }    
        }
        //��Ʈ�� ��Ұ� 1���� ������
        if (DotInfor.Count > 0)
            //���ӵ������� ü�¿� ������ ����
            target.MSetDamagedHP(DotValue);
    }

   //�ɸ� ���� �߰�
    // [0]: ������, [1]:������, [2]��� ����Ǵ���, [3]���� �ð�, [4]: ID
    public void FAddBuff(List<double> newBuff) 
    {
        //���� �̹� �ִ� �������
        if (BuffInfor.ContainsKey((int)newBuff[BuffId]))
        {
            //���ӽð��� ����
            if(BuffInfor[(int)newBuff[BuffId]][BuffLeft] < newBuff[BuffLeft])
                BuffInfor[(int)newBuff[BuffId]][BuffLeft] = newBuff[BuffLeft];
            return;
        }
        //���� �������� ��Ͽ� �߰�
        BuffInfor.Add((int)newBuff[BuffId], newBuff.GetRange(0, newBuff.Count-1));
        //���� ������ ���� ����
        AttatchedRawValue[(int)newBuff[BuffTarget]][BuffAdd] += newBuff[BuffAdd];
        AttatchedRawValue[(int)newBuff[BuffTarget]][BuffMulty] *= newBuff[BuffMulty];
        //���� ���� �� �� ����
        ProcessedStat[ (int)newBuff[BuffTarget] ] += (target.MOriginStat[BuffTarget] + AttatchedRawValue[(int)newBuff[BuffTarget]][0]) * AttatchedRawValue[(int)newBuff[BuffTarget]][0];
    }

    public void FAddBuff(double AddVal, double MultiVal, double Target, double left, double Id)
    {
        //�� �Լ� ������, �׳� �迭�� �ƴѰ� �迭�� ��ü�ؼ� ���
        FAddBuff(new List<double> { AddVal, MultiVal, Target, left, Id});
    }

   //�ɸ� ���ӵ� �߰�
    public void FAddDot(List<double> newDot)
    {
        //[0]ID, [1]������, [2]���ӽð�  
        DotInfor.Add(newDot);
    }
    public void FAddDot(double id, double damage, double time)
    {
        //�� �Լ� �����ε�, �迭�� �ƴϾ �ǵ���
        FAddDot(new List<double> { id, damage, time});
    }

    ////�������� ��������
    //public void FFixBuff()
    //{

    //}
}
