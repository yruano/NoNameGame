using System.Collections;
using UnityEngine;

//단순히 이동속도를 가속시키는 스크립트
public class MonsterSpeedUpSkill : MonoBehaviour
{
    public float MCoolTimeLeft;
    public float MUseTimeLeft;
    private Monster monster;
    Animator animator;
    [SerializeField]
    public float MSkillUseTime = 1f;//1초
    [SerializeField]
    public float MSkillCooltime = 5f;//5초
    [SerializeField]
    public float MDashForce = 2.0f;

    private void Start()
    {
        monster = GetComponent<Monster>();

        animator = GetComponent<Animator>();

        MCoolTimeLeft = 0f;
    }
    
    public void DoDash()
    {
        //MonoBehaviour 상속받은 곳만 이거 쓸 수 있어서 이런 구조가됨
        StartCoroutine(FDash());
    }

    public IEnumerator FDash()
    {
        Debug.Log("tried to SpeedUp");
        //쿨타임이 0이 아니면
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Speedup is on cooltime");
            //사용 거부
            yield break;
        }

        //대쉬 사용불가 기록
        animator.SetBool("isCanDash", false);

        //남은 시간을 쿨타임만큼으로 지정
        MCoolTimeLeft = MSkillCooltime;
        MUseTimeLeft = MSkillUseTime;
        //대쉬 스킬 사용으로 속도 빨라짐
        Debug.Log("SpeedUp!");
        monster.MSpeed *= MDashForce;
        //작동시간 처리
        while (MUseTimeLeft > 0f)
        {
            Debug.Log("usetime -0.1");
            MUseTimeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("return to original speed");
        //대쉬종료
        monster.MSpeed /= MDashForce;

        while (MCoolTimeLeft > 0f)
        {
            Debug.Log("cooltime -0.1");
            MCoolTimeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        //대쉬 사용가능 기록
        animator.SetBool("isCanDash", true);

        yield break;
    }
}