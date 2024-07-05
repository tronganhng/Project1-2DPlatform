using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_walkTest : StateMachineBehaviour
{
    Transform player, tran;
    Rigidbody2D rb;
    Enemy_MoveTest knight;
    public float speed, idle_range;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knight = animator.GetComponent<Enemy_MoveTest>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        tran = animator.GetComponent<Transform>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (knight.detect)
        {
            if (!animator.GetBool("freeze")) knight.LookAtPlayer();

            Vector2 target = new Vector2(player.position.x, rb.position.y);
            if (knight.IsGround() && !animator.GetBool("freeze"))
            {
                tran.position = Vector2.MoveTowards(tran.position, target, Time.deltaTime * speed);
            }

            if (Vector2.Distance(player.position, rb.position) <= idle_range)
            {
                animator.SetBool("stop", true);
            }
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
