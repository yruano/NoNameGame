using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBuff : Skill
{
    public const int ADD = 3;
    public const int MULTI = 4;
    public const int TARGETSTAT = 5;
    public const int DURATION = 6;

    //덧셈 연산값
    public int SBuffAdd = 0;
    //곱셈 연산값
    public int SBuffMulty = 1;
    //목표 스탯
    public int SBuffTargetStat = 0;
    //지속시간
    public int SBuffLeft = 0;

    public virtual void Awake()
    {
        //버프효과
        Type = 2;
    }

    //연산시 필요한 값 반환
    override public List<double> SFunction()
    {
        //기본 리스트를 가져오고
        List<double> tmp = base.SFunction();
        //추가할 리스트를 덧붙인 뒤
        tmp.AddRange(new List<double> { SBuffAdd, SBuffMulty, SBuffTargetStat, SBuffLeft });
        //호출한곳에 반환
        return tmp;
    }
}
