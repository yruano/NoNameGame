using System.Collections;
using UnityEngine;

public class MonsterRushSkill : MonoBehaviour
{
    private Monster monster;
    private MonsterTargetFinder MonsterTarget;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    public float MSkillCooltime = 5.0f;//5��
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

    //�ڷ�ƾ�� �̺�Ʈ �߻��� ȣ���� �Լ��� ����ϵ� ���ϰ�, �ڷ�ƾ ���� using������ �� ȣ�� ���ؼ� �̷� ������ ����
    public void DoRush()
    {
        StartCoroutine(FDash());
    }
    public IEnumerator FDash()
    {
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Rush is on cooltime");
            //��� �ź�
            yield break;
        }
        //�뽬 ���Ұ� ���
        animator.SetBool("isCanDash", false);
        //��Ÿ�� ����
        MCoolTimeLeft = MSkillCooltime;
        //�뽬 ��ų ���
        Debug.Log("Rush!");
        //���� ���ϰ�
        Vector3 direction = MonsterTarget.Mtarget.transform.position - transform.position;
        //�뽬��ų ����
        rb.AddForce(direction * MDashForce, ForceMode2D.Impulse);
        //�뽬��ų ��� ����

        //��Ÿ�� ���
        while (MCoolTimeLeft > 0f)
        {
            Debug.Log("cooltime -0.1");
            MCoolTimeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


        //�뽬 ��밡�� ���
        animator.SetBool("isCanDash", true);
        yield break;
    }
    /*
    public IEnumerator FDash()
    {
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Rush is on cooltime");
            //��� �ź�
            yield break;
        }
        //�뽬 ���Ұ� ���
        animator.SetBool("isCanDash", false);
        //��Ÿ�� ����
         MCoolTimeLeft = MSkillCooltime;
        //�뽬 ��ų ���
        Debug.Log("Rush!");
        //���� ���ϰ�
        Vector3 direction = MonsterTarget.Mtarget.transform.position - transform.position;
        //�뽬��ų ����
        rb.AddForce(direction * MDashForce, ForceMode2D.Impulse);
        //�뽬��ų ��� ����

        //��Ÿ�� ���
        while (MCoolTimeLeft > 0f)
        {
            Debug.Log("cooltime -0.1");
            MCoolTimeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


        //�뽬 ��밡�� ���
        animator.SetBool("isCanDash", true);
        yield break;
    }
    */
}
