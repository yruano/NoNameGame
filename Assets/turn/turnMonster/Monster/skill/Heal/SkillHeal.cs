using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHeal : Skill
{
    public const int AMOUNT = 3;
    [SerializeField]
    //기본적으로 가지는 힐량
    public double SHAmount = 0;

    public virtual void Awake()
    {
        //힐효과
        Type = 4;
    }

    //기본연산 담당함수, 치료할 양 반환, 컨트롤러에게 전달할 함수(메소드)
    //연산시 필요한 값 반환
    override public List<double> SFunction()
    {
        //기본 리스트를 가져오고
        List<double> tmp = base.SFunction();
        //추가할 리스트를 덧붙인 뒤
        tmp.Add(SHAmount);
        //호출한곳에 반환
        return tmp;
    }
}
