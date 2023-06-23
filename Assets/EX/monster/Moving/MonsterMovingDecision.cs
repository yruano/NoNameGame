using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ͱ� �÷��̾ �����ߴ��� üũ�ϴ� ��ũ��Ʈ.
public class MonsterMovingDecision : MonoBehaviour
{
    //���� ��ǥ
    [SerializeField]
    public Transform player; //�÷��̾�
    //���� ����
    [SerializeField]
    public float followRange = 5.0f;
    //������ ��ũ��Ʈ, ���� �������̽� ���� ����
    [SerializeField]
    public MonsterFollowInX MFollowInX;
    Animator animator;
    private void Start()
    {
        player = GetComponent<MonsterTargetFinder>().Mtarget.transform;
        animator = GetComponent<Animator>();
        MFollowInX = GetComponent<MonsterFollowInX>();
    }
    private void Update()
    {
        //���� ���� �� ������ ���� isMoving�� ������ -> Walk���� ����, �ָ� �ִٸ� walk���� Ż��
        if (Vector2.Distance(transform.position, player.position) < followRange)
        {
            animator.SetBool("isMoving", true);
            Debug.Log("tried to move");
        }
        else 
        {
            animator.SetBool("isMoving", false);
            Debug.Log("tried to stop");
        }
    }
} 
