using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_idleTest : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform player;
    Enemy_MoveTest knight;
    public float walk_range;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knight = animator.GetComponent<Enemy_MoveTest>();
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("freeze")) knight.LookAtPlayer();
        if (Vector2.Distance(player.position, rb.position) >= walk_range || !knight.detect)
        {
            animator.SetBool("stop", false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
