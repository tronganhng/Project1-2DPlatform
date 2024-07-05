using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioClip laugh,melee,atk_voice1,atk_voice2,smash, breath,angry, die;
    [SerializeField] private LayerMask playerlayer;
    [SerializeField] private Transform Atk1_Point, thunder_point;
    [SerializeField] private GameObject Thunder;
    public CameraShake mycamera;
    [SerializeField] private float atk1_Dame, Atk1_Range, atk_rate, callfire_rate, Angry_Heal;
    private float Attack1Time, Attack3Time; // bien thoi gian chay
    public bool inatk;
    private Transform player;
    private Animator ani;
    private WaterMovement pm;
    private Boss_Heal boss;
    private Player_State ps;
    GameController gamecontroll;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
        pm = FindObjectOfType<WaterMovement>();
        ps = FindObjectOfType<Player_State>();
        boss = GetComponent<Boss_Heal>();
        gamecontroll = FindObjectOfType<GameController>();
        SFXSource.PlayOneShot(laugh);
    }

    private void Update()
    {
        if (ani.GetBool("stop") && !ani.GetBool("freeze")) // neu dang idle
        {
            if(Time.time >= Attack1Time && pm.Heal > 0f && !inatk && !ps.Inulti) // ko atk khi player dang ulti
            {
                if(boss.currentHeal > Angry_Heal) Attack1Time = Time.time + Random.Range(atk_rate - 0.5f, atk_rate + 0.5f); // rate luc bth
                if(boss.currentHeal <= Angry_Heal) Attack1Time = Time.time + Random.Range(atk_rate - 1.3f, atk_rate - 0.2f); // rate luc angry
                int random_skill = Random.Range(1, 100); // chon random de thuc hien atk
                if(random_skill <= 35) // atk1 ti le 35% 
                {                    
                    ani.SetTrigger("atk1");
                    if(!ani.GetBool("angry")) SFXSource.PlayOneShot(atk_voice1);
                    else SFXSource.PlayOneShot(atk_voice2);
                }
                else if(random_skill > 35 && random_skill <= 70)
                {
                    ani.SetTrigger("jumpsk"); // jumpSmash ti le 35%
                }
                else
                {
                    if (boss.currentHeal > Angry_Heal) 
                    {
                        ani.SetTrigger("atk1");  //atk1 ti le 30% neu bth
                        if(!ani.GetBool("angry")) SFXSource.PlayOneShot(atk_voice1);
                        else SFXSource.PlayOneShot(atk_voice2);
                    }
                    
                    if (boss.currentHeal <= Angry_Heal) ani.SetTrigger("breath"); // breath ti le 30% neu angry
                }
            }
        }



        if(Time.time >= Attack3Time && pm.Heal > 0f && boss.currentHeal <= 900f && !inatk && !ps.Inulti && !ani.GetBool("freeze")) // call fireball
        {
            Attack3Time = Time.time + callfire_rate;
            ani.SetBool("callfire", true);
        }

        if(boss.currentHeal <= Angry_Heal && !ani.GetBool("angry")) //chuyen sang stage 2 (angry)
        {
            gamecontroll.musicSource.clip = gamecontroll.Boss_bg2;
            gamecontroll.musicSource.Play();
            SFXSource.PlayOneShot(angry);
            ani.SetBool("angry", true);
        }
    }

    public void LookAtPlayer() // flip Boss
    {
        if(transform.position.x > player.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void Atk1()
    {
        SFXSource.PlayOneShot(melee);
        // tao 1 vong tron kiem tra va cham voi layer player
        Collider2D[] hitEnemies_1 = Physics2D.OverlapCircleAll(Atk1_Point.position, Atk1_Range, playerlayer);
        //dame
        foreach (Collider2D player in hitEnemies_1)
        {
            ShakeCam2();
            player.GetComponent<WaterMovement>().Spawn_bloodEff();
            if(!ani.GetBool("angry")) player.GetComponent<WaterMovement>().TakeDame(atk1_Dame); 
            if(ani.GetBool("angry")) player.GetComponent<WaterMovement>().TakeDame(atk1_Dame + 14f);
        }
    }

    void SpawnThunder()
    {
        Instantiate(Thunder, thunder_point.position ,transform.rotation);
    }

    void SmashSFX(){
        SFXSource.PlayOneShot(smash);
    }

    void BreathSFX(){
        SFXSource.PlayOneShot(breath);
    }

    void ShakeCam()
    {
        mycamera.Shake(4.8f, 0.18f);
    }

    void ShakeCam2()
    {
        mycamera.Shake(5.8f, 0.24f);
    }

    void SetAtk()
    {
        inatk = true;
    }

    void OutAtk()
    {
        inatk = false;
    }

    /*
    private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(Atk1_Point.position, Atk1_Range);
    }*/
}
