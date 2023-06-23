using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : Skill
{
    public const int DAMAGE = 3;
    [SerializeField]
    //기본적으로 가지는 데미지
    public double SADamage = 0;

    public virtual void Awake()
    {
        //공격효과
        Type = 1;
    }

    //기본연산 담당함수, 깎을 체력 반환, 컨트롤러에게 전달할 함수(메소드)
    //연산시 필요한 값 반환
    override public List<double> SFunction()
    {
        //기본 리스트를 가져오고
        List<double> tmp = base.SFunction();
        //추가할 리스트를 덧붙인 뒤
        tmp.Add(SADamage);
        //호출한곳에 반환
        return tmp;
    }
}
