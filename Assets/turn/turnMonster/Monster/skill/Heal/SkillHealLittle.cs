using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHealLittle : SkillHeal
{
    new private void Awake()
    {
        base.Awake();
        //1��° ��(4)ȿ�� ��ų
        Id = 14;
        //ġ����� 10
        SHAmount = 10;
    }
}
