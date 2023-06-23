using System.Collections;
using System.Threading;
using UnityEngine;

//�浹 �߻��� ȣ���.
public class MeleeAttack : MonoBehaviour
{
    public bool AReadyForAttack;
    private Monster monster;
    public Entity ATarget;
    private void Start()
    {
        monster = GetComponent<Monster>();
        AReadyForAttack = true;
        //�̺�Ʈ �߻����� �޼ҵ� ���
        MeleeAttackChecker.MonsterMeleeAttack += FExecuteAttack;
    }

    public IEnumerator FExecuteAttack(Entity NewTarget)
    {
        Debug.Log("Melee Attack Called");
        if (!AReadyForAttack)
        {
            Debug.Log("Melee Attack is on CoolTime");
            //���ݼӵ���ŭ�� ���½ð��� �ȳ����µ��� ȣ��Ǹ� ���ݽõ� ����, �Լ�����
            yield break;
        }

        //���� ��ǥ ���
        ATarget = NewTarget;

        //Ÿ�� ��� �ȵǾ����� ��
        if (ATarget == null)
        {
            Debug.Log("Target not found!");
            yield break;
        }

        //���� �Լ����� �ٸ� �Լ��� ���� ����
        AReadyForAttack = false; 

        Debug.Log("Execute Melee Attack");

        //�ǰ����� �ǰ� �Լ��� �ҷ���, �������� �Ű�������
        //ũ��Ƽ�� �߻��� �߰����� �������� ��
        //!!���� Hited�� ���Ǿ��� ������� ����. ������ �ʿ���!!
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
        /*�ǰ����� ü���� ���� ����
        if (ACritChance > Random.Range(0f, 1f))
        {
            Debug.Log("Critical Damage");
            //ũ��Ƽ�� �߻��� �߰����� �������� ��
            ATarget.HP -= (ADamage * ACritDamage) - ATarget.Armor;
        }
        else
        {
            Debug.Log("nomale Damage");
            ATarget.HP -= ADamage - ATarget.Armor;
        }
        */
        yield return new WaitForSeconds(monster.MAttackRate);
        //���� �ٸ� �Լ��� ���ݰ���
        AReadyForAttack = true;
    }
}
