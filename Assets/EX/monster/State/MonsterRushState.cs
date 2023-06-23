using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRushState : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Monster monster;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //초기화
        rb = animator.GetComponent<Rigidbody2D>();
        monster = animator.GetComponent<Monster>();
        //스킬 사용
        animator.gameObject.GetComponent<MonsterRushSkill>().DoRush();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //대쉬상태에서 복귀할 상황 확인(속도가 기존 이동속도아래로 내려가야 방향 전환 가능하게 할 것
        if (stateInfo.IsName("Rush"))
        {                
            if (rb.velocity.magnitude <= monster.MSpeed)
            {
                Debug.Log("stop Rush");
                animator.SetTrigger("StopRush");
            }
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
