using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_shield : StateMachineBehaviour
{
    [SerializeField] private float shield_time, random_range;
    private float STime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        STime = Random.Range(shield_time - random_range, shield_time + random_range);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<Enemy_HealTest>().currentHeal <= 0f)
        {
            animator.SetBool("shield", false);
        }

        if(STime <= 0f)
        {
            animator.SetBool("shield", false);
        }
        animator.GetComponent<Enemy_MoveTest>().LookAtPlayer();
        STime -= Time.deltaTime;

    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }    
}
