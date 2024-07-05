using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    public Transform Atk1_Point, Atk2_Point, Atk3_Point, Ulti_Point, move_point;
    public GameObject Tonado, Enemy_hitEff;
    [SerializeField] private LayerMask enemyLayer, bossLayer, boxLayer;

    private WaterMovement playmove;
    private Player_State ps;
    [SerializeField] private SkillUI_Wind skillui;
 
    private float Attacktime1 = 0f, Attacktime2 = 0f, Attacktime3 = 0f, UltiTime = 0f; // bien tgian chay
    [SerializeField] private float Atk1_Range, Atk2_Range, AirAtk_Range, Ulti_Range; // ban kinh chem
    public float Atk1_Rate, Atk2_Rate, Atk3_Rate, Ulti_Rate; //tgian toi thieu 2 lan chem
    [SerializeField] private float atk1_Dame, atk2_Dame, ulti_Dame;

    public Mana_Bar mana_bar;
    private WaterAudio audioManager;
    CameraShake mycamera;
    SwitchCamera switchCamera;
    void Start()
    {
        playmove = GetComponent<WaterMovement>();
        ps = GetComponent<Player_State>();
        ani = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<WaterAudio>();
        mycamera = FindObjectOfType<CameraShake>();
        switchCamera = FindObjectOfType<SwitchCamera>();
    }

    void Update()
    {
        //Attack 1 & AirAttack:
        if (Time.time >= Attacktime1 && !playmove.isroll && !playmove.ishurt) //sau 1 tgian moi chem tiep duoc
        {
            if (Input.GetKeyDown(KeyCode.J) && playmove.IsGround() && !ps.Inatk)
            {
                ani.SetTrigger("atk1");
                Atk_Stuff(0f);
                Attacktime1 = Time.time + Atk1_Rate;
            }
            else if (Input.GetKeyDown(KeyCode.J) && !playmove.IsGround() && !ps.Inatk)
            {
                ani.SetTrigger("airatk");
                Atk_Stuff(0f);
                Attacktime1 = Time.time + Atk1_Rate;
            }
        }

        //Attack 2
        if (ps.current_mana >= 33f && Time.time >= Attacktime2)
        {
            if (Input.GetKeyDown(KeyCode.K) && playmove.IsGround() && !ps.Inulti && !playmove.isroll)
            {
                ani.SetTrigger("atk2");
                Atk_Stuff(33f);
                skillui.Ability1();
                Attacktime2 = Time.time + Atk2_Rate;
            }
        } 

        //Attack 3:
        if (Time.time >= Attacktime3 && ps.current_mana >= 15f)
        {
            if (Input.GetKeyDown(KeyCode.U) && !ps.Inulti && !playmove.isroll)
            {
                ani.SetTrigger("atk3");
                Atk_Stuff(12f);
                skillui.Ability2();
                Attacktime3 = Time.time + Atk3_Rate;
            }
        }

        //Ultimate:
        if(Time.time >= UltiTime)
        {
            if (Input.GetKeyDown(KeyCode.I) && ps.current_mana >= 55f && !ps.Inatk && !playmove.isroll)
            {
                ani.SetTrigger("ulti");
                Atk_Stuff(55f);
                skillui.Ability3();
                ps.Inulti = true;
                UltiTime = Time.time + Ulti_Rate;
            }
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

    void Attack1()
    {
        audioManager.PlaySFX(audioManager.melee);
        Vector2 force = transform.right * 1f;
        rb.AddForce(force, ForceMode2D.Impulse);
        // tao 1 vong tron kiem tra va cham voi layer enemy
        Collider2D[] hitEnemies_1 = Physics2D.OverlapCircleAll(Atk1_Point.position, Atk1_Range);
        //dame
        foreach (Collider2D enemy in hitEnemies_1) // kiem tra doi tuong quet thuoc layer nao
        {
            if(((1 << enemy.gameObject.layer) & enemyLayer) != 0)
            {
                if (enemy.GetComponent<Enemy_HealTest>().currentHeal > 0f)
                {
                    mycamera.Shake(1f, 0.1f);
                    Vector2 direct = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 1.5f, 3.3f);
                    enemy.GetComponent<Enemy_HealTest>().TakeDame(atk1_Dame);
                    if(enemy.GetComponent<Animator>().GetBool("shield") == false)  
                        enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
                }
            }
            if (((1 << enemy.gameObject.layer) & bossLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f);
                enemy.GetComponent<Boss_Heal>().TakeDame(atk1_Dame);
                enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
                audioManager.PlaySFX(audioManager.hit_enemy);
            }
            if (((1 << enemy.gameObject.layer) & boxLayer) != 0)
            {
                mycamera.Shake(1f, 0.1f);
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }

    }

    void Attack2_2()
    {
        audioManager.PlaySFX(audioManager.atk3);
        //tao vung attack
        Collider2D[] hitEnemies_2 = Physics2D.OverlapCircleAll(Atk2_Point.position, Atk2_Range);
        //dame
        foreach (Collider2D enemy in hitEnemies_2) // kiem tra doi tuong quet thuoc layer nao
        {
            if (((1 << enemy.gameObject.layer) & enemyLayer) != 0)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2.3f);
                enemy.GetComponent<Enemy_HealTest>().TakeDame(atk2_Dame);
                if(enemy.GetComponent<Animator>().GetBool("shield") == false)  
                        enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
            }
            if (((1 << enemy.gameObject.layer) & bossLayer) != 0)
            {
                enemy.GetComponent<Boss_Heal>().TakeDame(atk2_Dame);
            }
            if (((1 << enemy.gameObject.layer) & boxLayer) != 0)
            {
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Attack3()
    {
        audioManager.PlaySFX(audioManager.uskill);
        Instantiate(Tonado, Atk3_Point.position, Atk3_Point.rotation);
    }

    /*private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(Ulti_Point.position, Ulti_Range);
    }*/

    void Ultimate()
    {
        audioManager.PlaySFX(audioManager.ulti1);
        //tao vung attack
        Collider2D[] hitEnemies_2 = Physics2D.OverlapCircleAll(Ulti_Point.position, Ulti_Range);
        //dame
        foreach (Collider2D enemy in hitEnemies_2) // kiem tra doi tuong quet thuoc layer nao
        {
            if (((1 << enemy.gameObject.layer) & enemyLayer) != 0)
            {
                mycamera.Shake(1.5f, 0.13f);
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(move_point.right.x * 3.2f, 4.5f);
                enemy.GetComponent<Enemy_HealTest>().TakeDame(ulti_Dame);
                enemy.GetComponent<Enemy_HealTest>().SpawnHit_Eff(Enemy_hitEff);
            }
            if (((1 << enemy.gameObject.layer) & bossLayer) != 0)
            {
                mycamera.Shake(1.5f, 0.13f);
                enemy.GetComponent<Boss_Heal>().TakeDame(ulti_Dame);
                enemy.GetComponent<Boss_Heal>().SpawnHit_Eff();
            }
            if (((1 << enemy.gameObject.layer) & boxLayer) != 0)
            {
                mycamera.Shake(1.5f, 0.13f);
                enemy.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    void Atk_Stuff(float mana)
    {
        ps.Inatk = true;
        playmove.ishurt = false;
        rb.velocity = new Vector2(0f, 0f);
        ps.current_mana -= mana;
        mana_bar.SetMana(ps.current_mana);
    }

    void MeleeSFX(){
        audioManager.PlaySFX(audioManager.melee);
    }

    void OutAtk() // thoat khoi tt tan cong
    {
        ps.Inatk = false;
    }

    void OutUlti() // thoat khoi tt ulti
    {
        ps.Inulti = false;
    }
}
