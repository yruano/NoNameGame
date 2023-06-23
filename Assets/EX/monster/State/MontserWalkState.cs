using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//walk������ �� ���õ� ��ũ��Ʈ ȣ��
public class MontserWalkState : StateMachineBehaviour
{
    public MonsterFollowInX MFollowInX;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {//walk���� ���Խ� �̵� ��ũ��Ʈ ��
            animator.gameObject.GetComponent<MonsterFollowInX>().enabled = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {
            //�뽬�� �������� Ȯ���ؼ�
            if (animator.GetBool("isCanDash"))
            {
                // �뽬 ����
                //animator.gameObject.GetComponent<MonsterSpeedUpSkill>().DoDash();
                animator.gameObject.GetComponent<MonsterRushSkill>().DoRush();
                //MonsterDash?.Invoke(); �ڷ�ƾ�� �����Ű�� ����
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Walk"))
        {//walk���� ���� �� �̵� ��ũ��Ʈ ��
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
