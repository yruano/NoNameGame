using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArmorBuff : SkillBuff
{
    public override void Awake()
    {
        base.Awake();
        //1번째 버프(2)
        Id = 12;
        //목표: 방어력
        SBuffTargetStat = TurnMonster.ARMOR;
        //(+ 4)만큼 증가
        SBuffAdd = 4;
        //지속시간 = 4턴
        SBuffLeft = 4;
    }
}
