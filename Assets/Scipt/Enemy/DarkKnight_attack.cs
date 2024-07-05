using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight_attack : MonoBehaviour
{
    Animator ani;
    [SerializeField] private LayerMask playerlayer;
    [SerializeField] private GameObject Cut_Eff;
    [SerializeField] private Transform atk_point;
    [SerializeField] private float atk_dame, atk_range, atk_rate;
    private float AttackTime;
    private WaterMovement pm;
    EnemyAudio audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<EnemyAudio>();
        pm = FindObjectOfType<WaterMovement>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (ani.GetBool("stop") && !ani.GetBool("shield") && !ani.GetBool("freeze")) // dang dung yen va ko shield
        {
            if (Time.time >= AttackTime && pm.Heal > 0f)
            {
                int random_action = Random.Range(1, 11);
                if (random_action <= 6) ani.SetTrigger("atk"); // ti le chem 60%
                else ani.SetBool("shield", true); // ti le shield 40%
                AttackTime = Time.time + Random.Range(atk_rate - 0.3f, atk_rate + 0.2f);
            }
        }
    }

    void Atk()
    {
        audioManager.PlaySFX(audioManager.melee);
        // tao 1 vong tron kiem tra va cham voi layer player
        Collider2D[] hitEnemies_1 = Physics2D.OverlapCircleAll(atk_point.position, atk_range, playerlayer);
        //dame
        foreach (Collider2D player in hitEnemies_1)
        {
            player.GetComponent<WaterMovement>().Spawn_bloodEff();
            if(player.GetComponent<Player_State>().Inatk)    // chỉ chạy anim khi ko atk
            {
                player.GetComponent<WaterMovement>().TakeDame_NoAni(atk_dame);
            }
            else player.GetComponent<WaterMovement>().TakeDame(atk_dame);
            
            if(!player.GetComponent<Player_State>().Inulti)
            {
                Vector2 direct = (player.transform.position - transform.position).normalized;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 3.5f, 5.3f);
            }
        }
    }

    void SpawnCutEffect()
    {
        Instantiate(Cut_Eff, atk_point.position, atk_point.rotation);
    }

    /*private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(atk_point.position, atk_range);
    }*/
}
