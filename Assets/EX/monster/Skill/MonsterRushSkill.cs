using System.Collections;
using UnityEngine;

public class MonsterRushSkill : MonoBehaviour
{
    private Monster monster;
    private MonsterTargetFinder MonsterTarget;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    public float MSkillCooltime = 5.0f;//5초
    [SerializeField]
    public float MDashForce = 2.0f;
    public float MCoolTimeLeft;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        monster = GetComponent<Monster>();
        MonsterTarget = GetComponent<MonsterTargetFinder>();
    }

    //코루틴은 이벤트 발생시 호출할 함수로 등록하도 못하고, 코루틴 관련 using없으면 또 호출 못해서 이런 구조를 가짐
    public void DoRush()
    {
        StartCoroutine(FDash());
    }
    public IEnumerator FDash()
    {
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Rush is on cooltime");
            //사용 거부
            yield break;
        }
        //대쉬 사용불가 기록
        animator.SetBool("isCanDash", false);
        //쿨타임 갱신
        MCoolTimeLeft = MSkillCooltime;
        //대쉬 스킬 사용
        Debug.Log("Rush!");
        //방향 구하고
        Vector3 direction = MonsterTarget.Mtarget.transform.position - transform.position;
        //대쉬스킬 쓰고
        rb.AddForce(direction * MDashForce, ForceMode2D.Impulse);
        //대쉬스킬 사용 종료

        //쿨타임 대기
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
    /*
    public IEnumerator FDash()
    {
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Rush is on cooltime");
            //사용 거부
            yield break;
        }
        //대쉬 사용불가 기록
        animator.SetBool("isCanDash", false);
        //쿨타임 갱신
         MCoolTimeLeft = MSkillCooltime;
        //대쉬 스킬 사용
        Debug.Log("Rush!");
        //방향 구하고
        Vector3 direction = MonsterTarget.Mtarget.transform.position - transform.position;
        //대쉬스킬 쓰고
        rb.AddForce(direction * MDashForce, ForceMode2D.Impulse);
        //대쉬스킬 사용 종료

        //쿨타임 대기
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
    */
}
