using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackMultiHitWeak : SkillAttackMultiHit
{
    public override void Awake()
    {
        base.Awake();
        //1번째/다단히트인/공격
        Id = 191;
        //데미지 4
        SADamage = 4;
        //히트수 4
        SAHitCount = 4;
    }
}
