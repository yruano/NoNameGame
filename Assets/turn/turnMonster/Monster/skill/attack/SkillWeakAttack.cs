using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWeakAttack : SkillAttack 
{
    public override void Awake()
    {
        base.Awake();
        //2��°/�ٴ���Ʈ �ƴ� ����(1)
        Id = 201;
        SADamage = 10;
    }

}
