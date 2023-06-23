//on turn system, in battle, contain basic monter infor 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//턴제 시스템에서 전투중 사용할 몬스터의 기본정보들

public class TurnMonster : MonoBehaviour
{
    //전투준비 == 행동 가능한지
    public bool MReady = true;
    //인덱스와 배열방식 
    public List<double> MOriginStat = new List<double>{ 100.0, 100.0, 1.0, 1.0, 0.0, 0.0 }; //원본 스탯
    public const int MAXHP = 0;
    public const int NOWHP = 1;
    public const int DAMAGE = 2;
    public const int ARMOR = 3;
    public const int SPEED = 4;
    /*//개별 변수방식
    public double MMaxHp = 100.0; 
    public double MNowHp = 100.0;
    public double MDamage = 1.0;
    public double MArmor = 0.0;
    public double MSpeed = 1.0;
    //public double MRange = 1.0; 게임에 거리개념이 아직 없음
    //public double MPriority = 1.0;//우선순위?
    //public double luck = 1.0; 확률 변동 수치(현재 해당 개념 없음)*/

    //버프컨트롤러 연결
    public BuffController buffController;

    [SerializeField]
    //갖고 있는 스킬들을 저장. 
    public List<Skill> MGotSkill = new List<Skill>();
    //우선순위 범위 [0]최소 [1]최대, 숫자가 클수록 높은 우선순위
    public int[] priority = new int[2];

    //생성자
    public void Awake()
    {
        //버퍼 컨트롤과 연결
        buffController = GetComponent<BuffController>();
    }

   //입력 대응
    //버프 컨트롤러에 버프 계산인수 전달(배열버전)
    public void MSetRegistBuff(List<double> newBuff)
    {
        buffController.FAddBuff(newBuff);
    }
    
    //버프 컨트롤러에 버프 계산인수 전달(개별 인수 버전)
    public void MSetRegistBuff(double AddVal, double MultiVal, double Target, double left, double Id)
    {
        buffController.FAddBuff(AddVal, MultiVal, Target, left, Id);
    }
    
    //입력 받은 힐 값을 체력에 더함
    public void MSetHealHP(double val)
    {
        MOriginStat[NOWHP] += val;
    }
    
    //입력받은 데미지 값에 방어력 만큼을 뺄셈하고 체력값에 적용함
    public void MSetDamagedHP(double val)
    {
        MOriginStat[NOWHP] -= val - buffController.ProcessedStat[ARMOR];
    }
    
    //턴이 넘어갈때 호출될 거 호출함
    public void MNextTurn()
    {
        buffController.FNextTurn();
    }

   //출력 대응
    //버프 계산된 스탯 반환
    public List<double> MProcessedStat()
    {
        return buffController.ProcessedStat;
    }

    //사용할 스킬의 계산값에 우선순위 교체해 반환
    public List<double> MgetRandomSkill()
    {
        //가진 스킬중 하나를 랜덤으로 골라
        List <double> tmpSkill = MGotSkill[Random.Range(0, MGotSkill.Count)].SFunction();
        //갖고있는 우선순위 범위를 붙여
        tmpSkill[Skill.PRIORITY] = Random.Range(priority[0], priority[1]);
        //반환
        return tmpSkill;
    }
    
    //현재 체력 반환
    public double MGetLeftHP()
    {
        return MOriginStat[NOWHP];
    }
}
