using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    public float speed, idle_Range;
    Transform player;
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().LookAtPlayer(); // flip boss
        Vector2 target = new Vector2(player.position.x, rb.position.y); // vi tri boss di den
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.fixedDeltaTime * speed);
        rb.MovePosition(newPos);

        if(Vector2.Distance(rb.position, player.position) < idle_Range) // stop neu player vao vung idle_Range
        {
            animator.SetBool("stop", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("atk1"); // tranh lap lai atk1 nhieu lan
    }
}
