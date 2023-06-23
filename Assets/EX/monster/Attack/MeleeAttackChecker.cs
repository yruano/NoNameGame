///�浹 �߻��� ȣ���ؼ�, �浹 ��밡 �÷��̾�(Entity)�� �÷��̾ �����ϴ� �Լ� ȣ����

using System.Collections;
using UnityEngine;

public class MeleeAttackChecker : MonoBehaviour
{
    //private MeleeAttack Attacker; //������
    private Entity Target;   //�ǰ���

    //�̺�Ʈ
    public delegate IEnumerator MonsterMeleeAtteckEvent(Entity NewTarget);
    public static event MonsterMeleeAtteckEvent MonsterMeleeAttack;

    //private void Awake()
    //{
    //    //������ ������ �ʿ����
    //    Attacker = GetComponent<MeleeAttack>();
    //}

    //�� ��� ��� üũ�ϱ����� �̷� ������ ��,
    //�� �� Target�� �������ְ�, MonsterMeleeAttacx�̺�Ʈ�� �߻���Ŵ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision and MeleeAttackCheck");

        Target = collision.gameObject.GetComponent<Entity>();
        //��밡 Entity������Ʈ�� ������ ���� �� ���� �̺�Ʈ �߻�
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
        //��밡 Entity������Ʈ�� ������ ���� �� ���� �̺�Ʈ �߻�
        if (Target != null)
        {
            Debug.Log("Execute MeleeAttack");
            StartCoroutine(MonsterMeleeAttack(Target));
        }
    }
}
