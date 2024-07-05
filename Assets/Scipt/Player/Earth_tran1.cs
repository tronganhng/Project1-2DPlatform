using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Earth_tran1 : StateMachineBehaviour
{
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(animator.GetComponent<EarthCombat>().InputReceived)
       {
            animator.GetComponent<EarthCombat>().InputReceived = false;
            animator.GetComponent<EarthCombat>().SwitchInput();
            animator.GetComponent<Player_State>().Inatk = true;
            animator.SetTrigger("atk2");
       }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.ResetTrigger("atk1");
    }

}
