using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackMultiHit : SkillAttack
{
    public const int HITCOUNT = 4;
    //��Ʈ ��
    public double SAHitCount = 2;
    public override void Awake()
    {
        base.Awake();
        // �ٴ���Ʈ/����
        Id = 91;
    }
    override public List<double> SFunction()
    {
        //�⺻ ����Ʈ�� ��������
        List<double> tmp = base.SFunction();
        //�ڿ� ��Ʈ���� ���� ��
        tmp.Add(SAHitCount);
        //ȣ���Ѱ��� ��ȯ
        return tmp;
    }
}
