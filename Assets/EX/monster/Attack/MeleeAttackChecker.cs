///충돌 발생시 호출해서, 충돌 상대가 플레이어(Entity)면 플레이어를 공격하는 함수 호출함

using System.Collections;
using UnityEngine;

public class MeleeAttackChecker : MonoBehaviour
{
    //private MeleeAttack Attacker; //공격자
    private Entity Target;   //피격자

    //이벤트
    public delegate IEnumerator MonsterMeleeAtteckEvent(Entity NewTarget);
    public static event MonsterMeleeAtteckEvent MonsterMeleeAttack;

    //private void Awake()
    //{
    //    //공격자 지정이 필요없음
    //    Attacker = GetComponent<MeleeAttack>();
    //}

    //두 경우 모두 체크하기위해 이런 구조가 됨,
    //둘 다 Target을 연결해주고, MonsterMeleeAttacx이벤트를 발생시킴
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision and MeleeAttackCheck");

        Target = collision.gameObject.GetComponent<Entity>();
        //상대가 Entity컴포넌트를 가지고 있을 시 공격 이벤트 발생
        if (Target != null)
        {
            Debug.Log("Execute MeleeAttack");
            StartCoroutine(MonsterMeleeAttack(Target));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger and MeleeAttackCheck");

        Target = collision.gameObject.GetComponent<Entity>();
        //상대가 Entity컴포넌트를 가지고 있을 시 공격 이벤트 발생
        if (Target != null)
        {
            Debug.Log("Execute MeleeAttack");
            StartCoroutine(MonsterMeleeAttack(Target));
        }
    }
}
