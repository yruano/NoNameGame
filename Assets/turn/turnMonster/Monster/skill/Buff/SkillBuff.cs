using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBuff : Skill
{
    public const int ADD = 3;
    public const int MULTI = 4;
    public const int TARGETSTAT = 5;
    public const int DURATION = 6;

    //���� ���갪
    public int SBuffAdd = 0;
    //���� ���갪
    public int SBuffMulty = 1;
    //��ǥ ����
    public int SBuffTargetStat = 0;
    //���ӽð�
    public int SBuffLeft = 0;

    public virtual void Awake()
    {
        //����ȿ��
        Type = 2;
    }

    //����� �ʿ��� �� ��ȯ
    override public List<double> SFunction()
    {
        //�⺻ ����Ʈ�� ��������
        List<double> tmp = base.SFunction();
        //�߰��� ����Ʈ�� ������ ��
        tmp.AddRange(new List<double> { SBuffAdd, SBuffMulty, SBuffTargetStat, SBuffLeft });
        //ȣ���Ѱ��� ��ȯ
        return tmp;
    }
}
