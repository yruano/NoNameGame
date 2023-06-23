using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStrongAttack : SkillAttack
{
    public override void Awake()
    {
        base.Awake();
        //1번째/ 다단히트 아닌/ 공격
        Id = 101;
        SADamage = 25;
    }

}
