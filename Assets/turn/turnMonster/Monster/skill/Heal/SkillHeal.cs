using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHeal : Skill
{
    public const int AMOUNT = 3;
    [SerializeField]
    //�⺻������ ������ ����
    public double SHAmount = 0;

    public virtual void Awake()
    {
        //��ȿ��
        Type = 4;
    }

    //�⺻���� ����Լ�, ġ���� �� ��ȯ, ��Ʈ�ѷ����� ������ �Լ�(�޼ҵ�)
    //����� �ʿ��� �� ��ȯ
    override public List<double> SFunction()
    {
        //�⺻ ����Ʈ�� ��������
        List<double> tmp = base.SFunction();
        //�߰��� ����Ʈ�� ������ ��
        tmp.Add(SHAmount);
        //ȣ���Ѱ��� ��ȯ
        return tmp;
    }
}
