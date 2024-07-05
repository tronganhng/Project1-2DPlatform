using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("atk3");
        animator.ResetTrigger("airatk");
        animator.GetComponent<PlayerCombat>().CanReceiveInput = true;
        animator.GetComponent<Player_State>().Inatk = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<PlayerCombat>().InputReceived)
        {
            animator.GetComponent<PlayerCombat>().SwitchInput();
            animator.GetComponent<PlayerCombat>().InputReceived = false;
            animator.SetTrigger("atk1");
            animator.GetComponent<Player_State>().Inatk = true;
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

}
