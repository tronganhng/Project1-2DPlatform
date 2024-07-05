using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_idle : StateMachineBehaviour
{
    public float walk_Range; // vung walk
    Transform player;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // tham chieu
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().LookAtPlayer();
        if (Vector2.Distance(rb.position, player.position) >= walk_Range) // player out vung stop -> bosswalk 
        {
            animator.SetBool("stop", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    
}
