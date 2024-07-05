using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_attack : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioClip melee, cast, die;
    [SerializeField] private Transform atk_point;
    private WaterMovement pm;
    [SerializeField] private GameObject Spell;
    public CameraShake mycamera;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float atk_range, atk_dame, atk_rate, cast_rate; 
    private float AttackTime, Attack2Time;
    private Enemy_MoveTest emove;
    private Enemy_HealTest enemy;
    private Animator ani;
    private Player_State ps;
    private Transform player;

    private void Start()
    {
        enemy = GetComponent<Enemy_HealTest>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<WaterMovement>();
        ps = pm.GetComponent<Player_State>();
        player = pm.GetComponent<Transform>();
        emove = GetComponent<Enemy_MoveTest>();
        ani = GetComponent<Animator>();

    }

    void Update()
    {
        if (ani.GetBool("stop") && !ani.GetBool("freeze"))
        {
            if(Time.time >= AttackTime && pm.Heal > 0f && !ps.Inulti)
            {
                ani.SetTrigger("atk");
                AttackTime = Time.time + atk_rate;
            }
        }

        if(Time.time >= Attack2Time && enemy.currentHeal <= 390f && emove.detect && pm.Heal > 0 && !ps.Inulti && !ani.GetBool("freeze"))
        {
            ani.SetBool("shield", true);
            Attack2Time = Time.time + cast_rate;
        }

        if(enemy.currentHeal <= 0 && !ani.GetBool("isdeath")){
            SFXSource.PlayOneShot(die);
        }
    }

    void Atk()
    {
        SFXSource.PlayOneShot(melee);
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(atk_point.position, atk_range, playerLayer);

        foreach(Collider2D player in hit_player)
        {
            if(player.GetComponent<Player_State>().Inatk)    // chỉ chạy anim khi ko atk
            {
                player.GetComponent<WaterMovement>().TakeDame_NoAni(atk_dame);
            }
            else player.GetComponent<WaterMovement>().TakeDame(atk_dame);
        }
    }

    void SpawnSpell()
    {
        SFXSource.PlayOneShot(cast);
        Vector2 pos = new Vector2(player.position.x, -47.27f);
        Vector2 pos2 = new Vector2(player.position.x - 4.1f, -47.27f);
        Vector2 pos3 = new Vector2(player.position.x + 4.1f, -47.27f);
        Instantiate(Spell, pos, Quaternion.identity);
        Instantiate(Spell, pos2, Quaternion.identity);
        Instantiate(Spell, pos3, Quaternion.identity);
    }

    void ShakeCam()
    {
        mycamera.Shake(3.5f, 0.15f);
    }

    void ShieldOff()
    {
        ani.SetBool("shield", false);
    }

    /*private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(atk_point.position, atk_range);
    }*/
}
