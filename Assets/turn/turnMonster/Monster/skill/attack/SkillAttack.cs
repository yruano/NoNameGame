using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : Skill
{
    public const int DAMAGE = 3;
    [SerializeField]
    //�⺻������ ������ ������
    public double SADamage = 0;

    public virtual void Awake()
    {
        //����ȿ��
        Type = 1;
    }

    //�⺻���� ����Լ�, ���� ü�� ��ȯ, ��Ʈ�ѷ����� ������ �Լ�(�޼ҵ�)
    //����� �ʿ��� �� ��ȯ
    override public List<double> SFunction()
    {
        //�⺻ ����Ʈ�� ��������
        List<double> tmp = base.SFunction();
        //�߰��� ����Ʈ�� ������ ��
        tmp.Add(SADamage);
        //ȣ���Ѱ��� ��ȯ
        return tmp;
    }
}
