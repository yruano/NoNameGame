using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackMultiHitWeak : SkillAttackMultiHit
{
    public override void Awake()
    {
        base.Awake();
        //1��°/�ٴ���Ʈ��/����
        Id = 191;
        //������ 4
        SADamage = 4;
        //��Ʈ�� 4
        SAHitCount = 4;
    }
}
