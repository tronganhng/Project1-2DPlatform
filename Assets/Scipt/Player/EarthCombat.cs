using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCombat : MonoBehaviour
{
    public Transform atk1_point, atk2_point,bump_point, ulti_point, ulti_point2;
    public GameObject Earth_wall, Earth_Bump;
    public Mana_Bar mana_bar;
    public EathSkill_UI skillui;
    public LayerMask EnemyLayer, BossLayer, BoxLayer;
    public float airatk_rate, ulti_rate, shield_rate, bump_rate;
    public float melee_dame, ulti_dame;
    public Vector2 Hitbox1, Hitbox2, Hitbox4, Hitbox5, AirHitbox;
    public bool InputReceived;
    public bool CanReceiveInput = true;

    public float ShieldTime;
    private float AirAttackTime, UltiTime, BumpTime;
    private Player_State ps;
    private WaterMovement move;
    private Animator ani;
    CameraShake mycamera;
    Rigidbody2D rb;
    WaterAudio audioManager;
    SwitchCamera switchCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mycamera = FindObjectOfType<CameraShake>();
        audioManager = FindObjectOfType<WaterAudio>();
        ani = GetComponent<Animator>();
        move = GetComponent<WaterMovement>();
        ps = GetComponent<Player_State>();
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

        if(Input.GetKeyDown(KeyCode.U) && move.IsGround() && !ps.Inulti && !ps.Incall && ps.current_mana >= 18f)    // Earth bump
        {
            if(Time.time >= BumpTime)
            {
                ps.current_mana -= 12f;
                mana_bar.SetMana(ps.current_mana);
                skillui.Ability1();
                BumpTime = Time.time + bump_rate;
                ani.SetTrigger("bump");
            }
        }

        if(Time.time >= ShieldTime)   // Shield
        {
            if (Input.GetKey(KeyCode.K) && ps.current_mana >= 35f)
            {
                ani.SetBool("shield", true);
            }
            else ani.SetBool("shield", false);
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

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J) && move.IsGround() && !move.ishurt && !ps.Inulti && !ps.Incall)
        {
            if(CanReceiveInput){
                InputReceived = true;
                CanReceiveInput = false;
            }
            else return;
        }
    }

    public void SwitchInput(){
        if(CanReceiveInput) CanReceiveInput = false;
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
                    Vector2 direct = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 1.5f, 4.5f);                
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f);
                if(enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame);
                audioManager.PlaySFX(audioManager.hit_enemy);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f);
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }

        }
    }

    void AirAtk(){
        audioManager.PlaySFX(audioManager.melee);
        Vector2 Pos = new Vector2(transform.position.x, transform.position.y + 1.87f);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(Pos, AirHitbox, 0f);

        foreach(Collider2D enemy in hit_enemy)
        {
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0)
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {                    
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0)
            {
                if(enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
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
        audioManager.PlaySFX(audioManager.atk3);
        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(atk2_point.position, Hitbox2, 0f);

        foreach (Collider2D enemy in hit_enemy)
        { 
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    Vector2 direct = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 2.5f, 8f);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(melee_dame + 17f);
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                if (enemy.GetComponent<Animator>().GetBool("freeze")) enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                enemy.GetComponent<Boss_Heal>().TakeDame(melee_dame + 5f);
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
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
                    StartCoroutine(Freeze(enemy.gameObject));   // Freeze off khi k dính Ult2
                    
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
        SpawnEarthWall();

        Collider2D[] hit_enemy = Physics2D.OverlapBoxAll(ulti_point2.position, Hitbox5, 0f);

        foreach (Collider2D enemy in hit_enemy)
        {
            
            if (((1 << enemy.gameObject.layer) & EnemyLayer) != 0) // Enemy
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(ulti_dame);
                    enemy.GetComponent<Animator>().SetBool("freeze", false);                   
                }
            }
            if (((1 << enemy.gameObject.layer) & BossLayer) != 0) // Boss
            {
                enemy.GetComponent<Boss_Heal>().TakeDame(ulti_dame);
                enemy.GetComponent<Animator>().SetBool("freeze", false);  
            }
            if (((1 << enemy.gameObject.layer) & BoxLayer) != 0) // Box
            {
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    IEnumerator Freeze(GameObject enemy)
    {
        enemy.GetComponent<Animator>().SetBool("freeze", true);
        yield return new WaitForSeconds(1f);
        if(enemy != null)
            enemy.GetComponent<Animator>().SetBool("freeze", false);
    }

    void ShakeCam()
    {
        if(mycamera != null)
            mycamera.Shake(2f, 0.15f);
    }

    void ShakeCam2()
    {
        if(mycamera != null)
            mycamera.Shake(3.5f, 0.2f);
    }

    // private void OnDrawGizmos()
    // {
    //     Vector2 Pos = new Vector2(transform.position.x, transform.position.y + 1.87f);
    //     Gizmos.DrawWireCube(Pos, AirHitbox);
    // }
    void SpawnEarthWall(){
        Instantiate(Earth_wall, ulti_point2.position, Quaternion.identity);
    }

    void SpawnEathBump(){
        audioManager.PlaySFX(audioManager.uskill);
        Instantiate(Earth_Bump, bump_point.position, bump_point.rotation);
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
