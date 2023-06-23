using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackMultiHit : SkillAttack
{
    public const int HITCOUNT = 4;
    //히트 수
    public double SAHitCount = 2;
    public override void Awake()
    {
        base.Awake();
        // 다단히트/공격
        Id = 91;
    }
    override public List<double> SFunction()
    {
        //기본 리스트를 가져오고
        List<double> tmp = base.SFunction();
        //뒤에 히트수를 적은 후
        tmp.Add(SAHitCount);
        //호출한곳에 반환
        return tmp;
    }
}
