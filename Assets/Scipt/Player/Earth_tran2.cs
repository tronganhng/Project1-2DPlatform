using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_tran2 : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(animator.GetComponent<EarthCombat>().InputReceived)
       {
            animator.GetComponent<EarthCombat>().InputReceived = false;
            animator.GetComponent<EarthCombat>().SwitchInput();
            animator.GetComponent<Player_State>().Inatk = true;
            animator.SetTrigger("atk3");
       }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.GetComponent<EarthCombat>().CanReceiveInput = false;
      animator.ResetTrigger("atk2");
    }
}
