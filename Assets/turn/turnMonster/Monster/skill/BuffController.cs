using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//어떤 버프가 걸려있는지, 어떤 계산이 되어있는지, 끝나기까지 몇턴이 남았는지 기록.
//버프는 항상 (기본값 + 덧셈값) * 곱셈값으로 계산됨,
public class BuffController : MonoBehaviour
{
    [SerializeField]
    private TurnMonster target;

    //[ID][] [0]덧셈값 [1]곱셈값 [2]어디에 적용되는가 (-> TurnMonster.상수로 정의되어있음) [3]남은 적용시간
    public Dictionary<int, List<double>> BuffInfor = new Dictionary<int, List<double>>();
    public const int BuffAdd = 0;
    public const int BuffMulty = 1;
    public const int BuffTarget = 2;
    public const int BuffLeft = 3;
    public const int BuffId = 4;
    //버프 적용 후 값
    public List<double> ProcessedStat;
    //버프 적용 전의 날것의 값 [0]덧셈 [1]곱셈
    public List<List<double>> AttatchedRawValue = new List<List<double>>();

    //[0]ID [1]데미지 [2]남은 지속시간
    public List<List<double>> DotInfor = new List<List<double>>();//지속데미지 정보
    public const int Dotid = 0;
    public const int DotDamage = 1;
    public const int DotLeft = 2;
    public double DotValue;//지속데미지로 감소될 체력 정보

    public void Awake()
    {
        //계산 대상 지정
        target = GetComponent<TurnMonster>();
        //적용 후 값을 초기화
        ProcessedStat = new List<double>(target.MOriginStat);
        //적용 전 값을 초기화
        for (int i = 0; i < ProcessedStat.Count; i++)
        {
            AttatchedRawValue.Add(new List<double> { 0.0, 1.0 });
        }
    }

   //걸린 버프수 반환
    public int Count()
    {
        return ProcessedStat.Count;
    }

   //한 턴이 넘어갈 때 필요한 연산
    //남은 적용시간 전부 -1해서 턴이 하나 넘어간걸 적용
    public void FNextTurn()
    {
        //버프연산
        for (int i = 0; i < BuffInfor.Count; i++)
        {
            int key = BuffInfor.Keys.ElementAt(i);
            //남은턴이 0이면 제거
            if (--BuffInfor[key][3] <= 0)
            {
                //버프 적용 전 값을 갱신 (스텟 + 덧셈값) * 곱셈값 
                AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][BuffAdd] -= BuffInfor[key][BuffAdd];
                AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][BuffMulty] /= BuffInfor[key][BuffMulty];
                //버프 적용 후 값을 갱신
                ProcessedStat[ (int)BuffInfor[key][BuffTarget] ] = (target.MOriginStat[BuffTarget] + AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][0]) * AttatchedRawValue[(int)BuffInfor[key][BuffTarget]][1];
                //목록에서 삭제
                BuffInfor.Remove(key);
                i--; //삭제됐으니 인덱스 위치 조정
                //남은 버프가 없다면 원본값으로 교체, 소수연산이라 오차 생길 수 있는거 바로잡아주기위해
                if(BuffInfor.Count <= 0)
                    ProcessedStat = target.MOriginStat;
            }
        }
        //지속데미지 연산
        for(int i = 0; i < DotInfor.Count; i++)
        {
            //남은 턴이 0이면 제거
            if (--DotInfor[i][DotLeft] <= 0)
            {
                //적용될 값 갱신
                DotValue -= DotInfor[i][DotDamage];
                //배열에서 제거
                DotInfor.RemoveAt(i);
                i--;
            }    
        }
        //도트뎀 요소가 1개라도 있으면
        if (DotInfor.Count > 0)
            //지속데미지로 체력에 데미지 입힘
            target.MSetDamagedHP(DotValue);
    }

   //걸린 버프 추가
    // [0]: 덧셈값, [1]:곱셈값, [2]어디에 적용되는지, [3]지속 시간, [4]: ID
    public void FAddBuff(List<double> newBuff) 
    {
        //만약 이미 있는 버프라면
        if (BuffInfor.ContainsKey((int)newBuff[BuffId]))
        {
            //지속시간만 갱신
            if(BuffInfor[(int)newBuff[BuffId]][BuffLeft] < newBuff[BuffLeft])
                BuffInfor[(int)newBuff[BuffId]][BuffLeft] = newBuff[BuffLeft];
            return;
        }
        //무슨 버프인지 목록에 추가
        BuffInfor.Add((int)newBuff[BuffId], newBuff.GetRange(0, newBuff.Count-1));
        //버프 적용전 값을 갱신
        AttatchedRawValue[(int)newBuff[BuffTarget]][BuffAdd] += newBuff[BuffAdd];
        AttatchedRawValue[(int)newBuff[BuffTarget]][BuffMulty] *= newBuff[BuffMulty];
        //버프 적용 후 값 갱신
        ProcessedStat[ (int)newBuff[BuffTarget] ] += (target.MOriginStat[BuffTarget] + AttatchedRawValue[(int)newBuff[BuffTarget]][0]) * AttatchedRawValue[(int)newBuff[BuffTarget]][0];
    }

    public void FAddBuff(double AddVal, double MultiVal, double Target, double left, double Id)
    {
        //위 함수 재정의, 그냥 배열이 아닌걸 배열로 교체해서 기록
        FAddBuff(new List<double> { AddVal, MultiVal, Target, left, Id});
    }

   //걸린 지속딜 추가
    public void FAddDot(List<double> newDot)
    {
        //[0]ID, [1]데미지, [2]지속시간  
        DotInfor.Add(newDot);
    }
    public void FAddDot(double id, double damage, double time)
    {
        //위 함수 오버로딩, 배열이 아니어도 되도록
        FAddDot(new List<double> { id, damage, time});
    }

    ////버프연산 수정사항
    //public void FFixBuff()
    //{

    //}
}
