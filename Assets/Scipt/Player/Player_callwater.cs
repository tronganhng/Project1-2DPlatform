using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_callwater : StateMachineBehaviour
{
    public GameObject water_ball;
    private Transform player;
    public float spawn_rate, use_time;
    private float CTime, UTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Transform>();
        animator.GetComponent<Player_State>().Incall = true;
        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UTime += Time.deltaTime;

        if(UTime >= use_time) // neu giu qua usetime thi huy chieu
        {
            UTime = 0f;
            animator.GetComponent<PlayerCombat>().skillui.Ability2();
            animator.GetComponent<Player_State>().current_mana -= 35f;
            animator.GetComponent<PlayerCombat>().mana_bar.SetMana(animator.GetComponent<Player_State>().current_mana);
            animator.GetComponent<PlayerCombat>().CallTime = Time.time + animator.GetComponent<PlayerCombat>().call_rate;
            animator.SetBool("callwater", false);
        }

        if(Time.time >= CTime) // spawn water ball
        {
            SpawnWaterBall();
            CTime = Time.time + spawn_rate;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player_State>().Incall = false;
    }

    void SpawnWaterBall()
    {
        Vector2 pos = new Vector2(Random.Range(player.position.x - 7f,player.position.x + 7f), player.position.y + 5f);
        Instantiate(water_ball, pos, Quaternion.Euler(0f, 0f, -90f));
    }
}
