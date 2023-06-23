using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터가 플레이어를 포착했는지 체크하는 스크립트.
public class MonsterMovingDecision : MonoBehaviour
{
    //추적 목표
    [SerializeField]
    public Transform player; //플레이어
    //추적 범위
    [SerializeField]
    public float followRange = 5.0f;
    //추적할 스크립트, 아직 인터페이스 결정 못함
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
        //범위 보다 더 가까이 있음 isMoving을 참으로 -> Walk상태 진입, 멀리 있다면 walk상태 탈출
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
