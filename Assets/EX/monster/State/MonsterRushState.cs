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
        //�ʱ�ȭ
        rb = animator.GetComponent<Rigidbody2D>();
        monster = animator.GetComponent<Monster>();
        //��ų ���
        animator.gameObject.GetComponent<MonsterRushSkill>().DoRush();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //�뽬���¿��� ������ ��Ȳ Ȯ��(�ӵ��� ���� �̵��ӵ��Ʒ��� �������� ���� ��ȯ �����ϰ� �� ��
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
