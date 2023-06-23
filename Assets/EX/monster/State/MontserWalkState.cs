using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//walk상태일 때 관련된 스크립트 호출
public class MontserWalkState : StateMachineBehaviour
{
    public MonsterFollowInX MFollowInX;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {//walk상태 진입시 이동 스크립트 켬
            animator.gameObject.GetComponent<MonsterFollowInX>().enabled = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {
            //대쉬가 가능한지 확인해서
            if (animator.GetBool("isCanDash"))
            {
                // 대쉬 실행
                //animator.gameObject.GetComponent<MonsterSpeedUpSkill>().DoDash();
                animator.gameObject.GetComponent<MonsterRushSkill>().DoRush();
                //MonsterDash?.Invoke(); 코루틴은 실행시키지 못함
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {//walk상태 나갈 때 이동 스크립트 끔
            animator.gameObject.GetComponent<MonsterFollowInX>().enabled = false;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
