using System.Collections;
using System.Threading;
using UnityEngine;

//충돌 발생시 호출됨.
public class MeleeAttack : MonoBehaviour
{
    public bool AReadyForAttack;
    private Monster monster;
    public Entity ATarget;
    private void Start()
    {
        monster = GetComponent<Monster>();
        AReadyForAttack = true;
        //이벤트 발생지에 메소드 등록
        MeleeAttackChecker.MonsterMeleeAttack += FExecuteAttack;
    }

    public IEnumerator FExecuteAttack(Entity NewTarget)
    {
        Debug.Log("Melee Attack Called");
        if (!AReadyForAttack)
        {
            Debug.Log("Melee Attack is on CoolTime");
            //공격속도만큼의 쉬는시간이 안끝났는데도 호출되면 공격시도 안함, 함수종료
            yield break;
        }

        //공격 목표 등록
        ATarget = NewTarget;

        //타겟 등록 안되어있을 시
        if (ATarget == null)
        {
            Debug.Log("Target not found!");
            yield break;
        }

        //현재 함수말고 다른 함수는 공격 금지
        AReadyForAttack = false; 

        Debug.Log("Execute Melee Attack");

        //피격자의 피격 함수를 불러옴, 데미지는 매개변수로
        //크리티컬 발생시 추가적인 데미지를 줌
        //!!여기 Hited는 상의없이 만들어진 곳임. 조율이 필요함!!
        if (monster.MCritChance > Random.Range(0f, 1f))
        {
            Debug.Log("Critical Damage");
            ATarget.FHited(monster.MDamage * monster.MCritDamage);
        }
        else
        {
            Debug.Log("nomale Damage");
            ATarget.FHited(monster.MDamage);
        }
        /*피격자의 체력을 직접 깎음
        if (ACritChance > Random.Range(0f, 1f))
        {
            Debug.Log("Critical Damage");
            //크리티컬 발생시 추가적인 데미지를 줌
            ATarget.HP -= (ADamage * ACritDamage) - ATarget.Armor;
        }
        else
        {
            Debug.Log("nomale Damage");
            ATarget.HP -= ADamage - ATarget.Armor;
        }
        */
        yield return new WaitForSeconds(monster.MAttackRate);
        //이제 다른 함수도 공격가능
        AReadyForAttack = true;
    }
}
