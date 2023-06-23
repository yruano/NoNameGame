using System.Collections;
using UnityEngine;

//�ܼ��� �̵��ӵ��� ���ӽ�Ű�� ��ũ��Ʈ
public class MonsterSpeedUpSkill : MonoBehaviour
{
    public float MCoolTimeLeft;
    public float MUseTimeLeft;
    private Monster monster;
    Animator animator;
    [SerializeField]
    public float MSkillUseTime = 1f;//1��
    [SerializeField]
    public float MSkillCooltime = 5f;//5��
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
        //MonoBehaviour ��ӹ��� ���� �̰� �� �� �־ �̷� ��������
        StartCoroutine(FDash());
    }

    public IEnumerator FDash()
    {
        Debug.Log("tried to SpeedUp");
        //��Ÿ���� 0�� �ƴϸ�
        if (MCoolTimeLeft > 0f)
        {
            Debug.Log("Speedup is on cooltime");
            //��� �ź�
            yield break;
        }

        //�뽬 ���Ұ� ���
        animator.SetBool("isCanDash", false);

        //���� �ð��� ��Ÿ�Ӹ�ŭ���� ����
        MCoolTimeLeft = MSkillCooltime;
        MUseTimeLeft = MSkillUseTime;
        //�뽬 ��ų ������� �ӵ� ������
        Debug.Log("SpeedUp!");
        monster.MSpeed *= MDashForce;
        //�۵��ð� ó��
        while (MUseTimeLeft > 0f)
        {
            Debug.Log("usetime -0.1");
            MUseTimeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("return to original speed");
        //�뽬����
        monster.MSpeed /= MDashForce;

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
}