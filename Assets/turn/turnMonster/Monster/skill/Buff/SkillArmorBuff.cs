using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArmorBuff : SkillBuff
{
    public override void Awake()
    {
        base.Awake();
        //1��° ����(2)
        Id = 12;
        //��ǥ: ����
        SBuffTargetStat = TurnMonster.ARMOR;
        //(+ 4)��ŭ ����
        SBuffAdd = 4;
        //���ӽð� = 4��
        SBuffLeft = 4;
    }
}
