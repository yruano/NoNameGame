using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStrongAttack : SkillAttack
{
    public override void Awake()
    {
        base.Awake();
        //1��°/ �ٴ���Ʈ �ƴ�/ ����
        Id = 101;
        SADamage = 25;
    }

}
