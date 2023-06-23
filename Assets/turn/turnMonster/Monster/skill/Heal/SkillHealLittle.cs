using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHealLittle : SkillHeal
{
    new private void Awake()
    {
        base.Awake();
        //1번째 힐(4)효과 스킬
        Id = 14;
        //치료양은 10
        SHAmount = 10;
    }
}
