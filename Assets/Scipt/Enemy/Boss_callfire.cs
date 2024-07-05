using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_callfire : StateMachineBehaviour
{
    [SerializeField] private float Call_Rate, Spawn_Rate;
    [SerializeField] private GameObject FireBall;
    private float calltime, spawntime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().inatk = true;
        calltime = Call_Rate;
        spawntime = Spawn_Rate;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        calltime -= Time.fixedDeltaTime;
        if(calltime <= 0f) // tgian thuc hien ani
        {
            animator.GetComponent<Boss>().inatk = false;
            calltime = Call_Rate;
            animator.SetBool("callfire", false);
        }

        spawntime -= Time.fixedDeltaTime;
        if (spawntime <= 0f) // tgian 2 lan spawn
        {
            SpawnFireBall(115.5f, 126.5f); // chia ra 3 khoang -> spawn deu hon
            SpawnFireBall(126.5f, 140f);
            SpawnFireBall(140f, 151.4f);
            spawntime = Spawn_Rate;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("callfire", false);
        calltime = Call_Rate;
    }

    void SpawnFireBall(float left, float right)
    {
        Vector2 FBall_Pos = new Vector2(Random.Range(left, right), 90f);
        Instantiate(FireBall, FBall_Pos, Quaternion.identity);
    }
}
