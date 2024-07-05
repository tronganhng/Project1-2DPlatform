using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform atk1_point, atk2_point, atk3_point, ulti_point, ulti_point2;
    public GameObject Waterblast, Enemy_hitEff;
    public Mana_Bar mana_bar;
    public SkillUI skillui;
    public float airatk_rate, call_rate, shoot_rate, ulti_rate;
    public float melee_dame, ulti_dame;
    public Vector2 Hitbox1, Hitbox2, Hitbox3, Hitbox4, Hitbox5;
    public LayerMask EnemyLayer, BossLayer, BoxLayer;
    public bool CanReceiveInput = true;
    public bool InputReceived;

    private float AirAttackTime, ShootTime, UltiTime;
    public float CallTime;
    private WaterMovement move;
    private Animator ani;
    private Player_State ps;
    Rigidbody2D rb;
    CameraShake mycamera;
    WaterAudio audioManager;
    SwitchCamera switchCamera;
    private void Awake()
    {
        mycamera = FindObjectOfType<CameraShake>();
        ps = GetComponent<Player_State>();
        ani = GetComponent<Animator>();
        move = GetComponent<WaterMovement>();
        audioManager = FindObjectOfType<WaterAudio>();
        rb = GetComponent<Rigidbody2D>();
        switchCamera = FindObjectOfType<SwitchCamera>();
    }

    void Update()
    {
        Attack();

        if(Time.time >= AirAttackTime)  // Air atk
        {
            if(Input.GetKeyDown(KeyCode.J) && !move.IsGround() && !ps.Incall)
            {
                AirAttackTime = Time.time + airatk_rate;
                ani.SetTrigger("airatk");
                ps.Inatk = true;
                ani.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }

        if(Input.GetKeyDown(KeyCode.I) && move.IsGround() && !ps.Incall && ps.current_mana >= 50f)   // Ulti
        {
            if(Time.time >= UltiTime)
            {
                ps.current_mana -= 50f;
                mana_bar.SetMana(ps.current_mana);
                skillui.Ability3();
                UltiTime = Time.time + ulti_rate;
                ani.SetTrigger("ulti");

            }
        }

        if(Input.GetKeyDown(KeyCode.U) && move.IsGround() && !ps.Inulti && !ps.Incall && ps.current_mana >= 18f)    // Water blast
        {
            if(Time.time >= ShootTime)
            {
                ps.current_mana -= 16f;
                mana_bar.SetMana(ps.current_mana);
                skillui.Ability1();
                ShootTime = Time.time + shoot_rate;
                ani.SetTrigger("shoot");
            }
        }

        if(Time.time >= CallTime)   // Call Water
        {
            if (Input.GetKey(KeyCode.K) && ps.current_mana >= 35f)
            {
                ani.SetBool("callwater", true);
            }
            else ani.SetBool("callwater", false);
        }

        if(switchCamera != null)
        {
            if(switchCamera.changed_cam) // tham chieu lai main_cam
            {
                switchCamera.changed_cam = false;
                mycamera = FindObjectOfType<CameraShake>();
            }
        }
    }

    public void Attack()    // Melee Combo
    {
        if(!move.ishurt && !ps.Inulti && !ps.Incall)
        {
            if (Input.GetKeyDown(KeyCode.J) && move.IsGround())
            {
                if (CanReceiveInput)
                {
                    CanReceiveInput = false;
                    InputReceived = true;
                }
                else return;
            }
        }
    }

    public void SwitchInput()
    {
        if (CanReceiveInput)
        {
            CanReceiveInput = false;
        }
        else CanReceiveInput = true;
    }

    void Atk1()
    {
        audioManager.PlaySFX(audioManager.melee);
        Vector2 force = transform.right * 0.8f;
        rb.AddForce(force, ForceMode2D.Impulse);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(atk1_point.position, Hitbox1, 0f);

        foreach(Collider2D enemy in hit_enemy)
        {
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0)
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {         
                    mycamera.Shake(1f, 0.1f); 
                    if(enemy.GetComponent<Animator>().GetBool("shield") == false)  
                        enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f); 
                enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f); 
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }

        }
    }

    void AirAtk()
    {
        audioManager.PlaySFX(audioManager.melee);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(atk2_point.position + new Vector3(0, 0.5f, 0), Hitbox2 * 1.1f, 0f);

        foreach (Collider2D enemy in hit_enemy)
        {
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0)
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    if(!enemy.GetComponent<Animator>().GetBool("shield")) 
                        enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0)
            {
                if (enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0)
            {
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Atk2()
    {
        audioManager.PlaySFX(audioManager.melee);
        Vector2 force = transform.right * 0.8f;
        rb.AddForce(force, ForceMode2D.Impulse);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(atk2_point.position, Hitbox2, 0f);

        foreach (Collider2D enemy in hit_enemy)
        { 
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    mycamera.Shake(1f, 0.1f);
                    Vector2 direct = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 2.5f, 7f);
                    if(!enemy.GetComponent<Animator>().GetBool("shield")) 
                        enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame + 5f);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                mycamera.Shake(1f, 0.1f);
                if (enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame + 5f);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
                mycamera.Shake(1f, 0.1f);
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Atk3()
    {
        audioManager.PlaySFX(audioManager.atk3);
        Vector2 force = transform.right * 0.8f;
        rb.AddForce(force, ForceMode2D.Impulse);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(atk3_point.position, Hitbox3, 0f);

        foreach (Collider2D enemy in hit_enemy)
        {           
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    mycamera.Shake(1f, 0.1f);
                    Vector2 direct = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 2.8f, 7f);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame + 15f);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                mycamera.Shake(1f, 0.1f);
                if (enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame + 15f);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
                mycamera.Shake(1f, 0.1f);
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Ulti1()
    {
        audioManager.PlaySFX(audioManager.ulti1);

        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(ulti_point.position, Hitbox4, 0f);

        foreach (Collider2D enemy in hit_enemy)
        {        
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * (ulti_point.position.x - enemy.transform.position.x),8f);

                    enemy.GetComponent<Enemy_HealTest>().TakeDame(ulti_dame);
                    enemy.GetComponent<Animator>().SetBool("freeze", true);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                enemy.GetComponent<Boss_Heal>().TakeDame(ulti_dame);
                enemy.GetComponent<Animator>().SetBool("freeze", true);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Ulti2()
    {
        audioManager.PlaySFX(audioManager.ulti2);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(ulti_point2.position, Hitbox5, 0f);

        foreach (Collider2D enemy in hit_enemy)
        {
            
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    enemy.GetComponent<Enemy_HealTest>().SpawnIce_Eff();
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(ulti_dame);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                enemy.GetComponent<Boss_Heal>().SpawnIce_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(ulti_dame);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(atk3_point.position, Hitbox3);
    // }

    void Shoot()
    {
        audioManager.PlaySFX(audioManager.uskill);
        Instantiate(Waterblast, transform.position, transform.rotation);
    }

    void ShakeCam()
    {
        if(mycamera != null)
            mycamera.Shake(3f, 0.15f);
    }

    void ShakeCam2()
    {
        if(mycamera != null)
            mycamera.Shake(3.9f, 0.25f);
    }

    void SetUlti()
    {
        ps.Inulti = true;
        ps.Inatk = true;
    }

    void SetAtk()
    {
        ps.Inatk = true;
    }

    void OutAtk()
    {
        ps.Inatk = false;
        ps.Inulti = false;
    }
}
