using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shield : StateMachineBehaviour
{
    public GameObject earth_bullet;
    private Transform player;
    public float spawn_rate, use_time;
    private float CTime, UTime;
    WaterAudio audioManager;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = FindObjectOfType<WaterAudio>();
        audioManager.PlaySFX(audioManager.kskill);
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
            animator.GetComponent<EarthCombat>().skillui.Ability2();
            animator.GetComponent<Player_State>().current_mana -= 35f;
            animator.GetComponent<EarthCombat>().mana_bar.SetMana(animator.GetComponent<Player_State>().current_mana);
            animator.GetComponent<EarthCombat>().ShieldTime = Time.time + animator.GetComponent<EarthCombat>().shield_rate;
            animator.SetBool("shield", false);
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
        Vector2 pos = new Vector2(player.position.x, Random.Range(player.position.y + 0.2f, player.position.y + 2.2f));
        Instantiate(earth_bullet, pos, player.rotation);
    }
}
