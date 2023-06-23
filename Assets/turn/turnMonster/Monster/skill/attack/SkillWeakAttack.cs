using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWeakAttack : SkillAttack 
{
    public override void Awake()
    {
        base.Awake();
        //2번째/다단히트 아닌 공격(1)
        Id = 201;
        SADamage = 10;
    }

}
