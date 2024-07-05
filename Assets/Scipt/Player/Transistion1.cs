using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistion1 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerCombat>().CanReceiveInput = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<PlayerCombat>().InputReceived)
        {
            animator.GetComponent<Player_State>().Inatk = true;
            animator.GetComponent<PlayerCombat>().InputReceived = false;
            animator.GetComponent<PlayerCombat>().SwitchInput();
            animator.SetTrigger("atk2");
        }    
    }

     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("atk1");
    }

}
