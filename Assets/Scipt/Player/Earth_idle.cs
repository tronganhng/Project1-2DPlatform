using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_idle : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("airatk");
        animator.ResetTrigger("atk3");
        animator.GetComponent<EarthCombat>().CanReceiveInput = true;
        animator.GetComponent<Player_State>().Inatk = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<EarthCombat>().InputReceived)
        {
            animator.GetComponent<EarthCombat>().InputReceived = false;
            animator.GetComponent<EarthCombat>().SwitchInput();
            animator.GetComponent<Player_State>().Inatk = true;
            animator.SetTrigger("atk1");
        }
    }

    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }    
}
