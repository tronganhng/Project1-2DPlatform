using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_attack : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioClip melee1, melee2, bullet, explode;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform atk1_point,atk2_point, shoot_point;
    private Rigidbody2D player;
    private WaterMovement pm;
    private Player_State ps;
    [SerializeField] private LayerMask playerLayer, boxLayer;
    [SerializeField] private float atk1_range, atk2_range, atk_rate, shoot_rate, explode_range, power_push;
    [SerializeField] private float atk1_dame, atk2_dame, explode_dame;
    private float AttackTime, Attack3Time;
    private Animator ani;
    private Enemy_MoveTest emove;
    private Enemy_HealTest enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        pm = player.GetComponent<WaterMovement>();
        ps = player.GetComponent<Player_State>();
        emove = GetComponent<Enemy_MoveTest>();
        ani = GetComponent<Animator>();
        enemy = GetComponent<Enemy_HealTest>();
    }

    void Start()
    {
        Attack3Time = 10f;
    }

    
    void Update()
    {
        // random atk1 or 2
        if(ani.GetBool("stop") && !ani.GetBool("freeze"))
        {
            if(Time.time >= AttackTime && pm.Heal > 0f && !ps.Inulti)
            {
                AttackTime = Time.time + atk_rate;
                int random = Random.Range(1, 11);
                if (random <= 6) ani.SetTrigger("atk1");
                else ani.SetTrigger("atk2");
            }
        }

        // nha dan
        if(Time.time >= Attack3Time && emove.detect && pm.Heal > 0f && !ps.Inulti && enemy.currentHeal <= 400f && !ani.GetBool("freeze"))
        {
            ani.SetBool("shield", true);
            Attack3Time = Time.time + shoot_rate;
        }
    }

    void Atk1()
    {
        SFXSource.PlayOneShot(melee1);
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(atk1_point.position, atk1_range, playerLayer);
        foreach(Collider2D player in hit_player)
        {
            if(player.GetComponent<Player_State>().Inatk)    // chỉ chạy anim khi ko atk
            {
                player.GetComponent<WaterMovement>().TakeDame_NoAni(atk1_dame);
            }
            else player.GetComponent<WaterMovement>().TakeDame(atk1_dame);
        }
    }
    void Atk2()
    {
        SFXSource.PlayOneShot(melee2);
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(atk2_point.position, atk2_range, playerLayer);
        foreach (Collider2D player in hit_player)
        {
            player.GetComponent<WaterMovement>().TakeDame(atk2_dame);
        }
    }

    void SpawnBullet()
    {
        SFXSource.PlayOneShot(bullet);
        Instantiate(Bullet, shoot_point.position, shoot_point.rotation);
    }

    void Explode()
    {
        SFXSource.PlayOneShot(explode);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 2f);
        Collider2D[] range = Physics2D.OverlapCircleAll(pos, explode_range);

        foreach (Collider2D character in range)
        {
            if (((1 << character.gameObject.layer) & playerLayer) != 0)
            {
                character.GetComponent<WaterMovement>().TakeDame(explode_dame);
                int direct;
                if (player.position.x <= transform.position.x) direct = -1;
                else direct = 1;
                player.velocity = new Vector2(3f * direct, 2f) * power_push;
            }
            if (((1 << character.gameObject.layer) & boxLayer) != 0)
            {
                character.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    /*private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 2f);
        Gizmos.DrawWireSphere(atk2_point.position, atk2_range);
    }*/
}
