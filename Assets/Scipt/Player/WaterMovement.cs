using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    private Player_State ps;
    private Animator anim;
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;
    public GameObject run_effect, jump_effect, roll_effect, Pop_Text, BloodEff;
    public Heal_Bar heal_bar;

    [SerializeField] private LayerMask jumpGround;
    [SerializeField] private LayerMask Trap;

    public float speed, jumphigh, roll_speed, roll_Rate;
    private int jumpcharge; // double jump
    private float DirX = 0f;

    public float maxheal = 200, Heal;

    private float Hurttime = 0f, roll_time, RollTime_Run = 0f; //RollTime_run: bien tgian chay
    public Vector2 Boxsize;
    public float checkDistance;
    public bool ishurt, isroll;
    private Vector2 Roll_Direct;

    AudioManager audio_manager;
    WaterAudio waterAudio;

    private enum MovementState { idle, running, up, down, die }  //tao ra data type

    void Awake()
    {
        roll_time = 0.37f;
        Heal = maxheal;
        heal_bar.SetMaxHeal(maxheal);
        ps = GetComponent<Player_State>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        audio_manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        waterAudio = GetComponentInChildren<WaterAudio>();
    }

    void Update()
    {
        // Move X
        DirX = Input.GetAxisRaw("Horizontal");    //getAxis: giam dan ve 0 khi tha a,d; getAxisRaw: nhay ve 0
        if (/*ps.Inatk ||*/ ps.Incall) DirX = 0f;  // k the move khi van skill
        if (!ps.Inatk)
            if (!ishurt)  rb.velocity = new Vector2(DirX * speed, rb.velocity.y);

        // Jump
        if ((IsGround() || IsHurtbyTrapy()) && rb.velocity.y <= 0) jumpcharge = 2;

        if (Input.GetButtonDown("Jump") && jumpcharge > 0 && !ps.Inatk && !ps.Incall && !isroll)
        {
            audio_manager.PlaySFX(audio_manager.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumphigh);
            Spawn_jumpEff();
            jumpcharge--;
        }

        // Roll
        if (Time.time >= RollTime_Run)
        {
            if (Input.GetKeyDown(KeyCode.L) && !ps.Inatk && !ps.Incall)
            {
                audio_manager.PlaySFX(audio_manager.dash);
                anim.SetTrigger("roll");
                Spawn_rollEff(); // tao hieu ung lon
                isroll = true;
                Roll_Direct = transform.right;
                RollTime_Run = Time.time + roll_Rate;
            }
        }
        if (isroll)
        {
            roll_time -= Time.deltaTime;
            rb.velocity = Roll_Direct * roll_speed;
            if (roll_time <= 0 || ishurt)
            {
                rb.velocity = new Vector2(0, 0);
                anim.ResetTrigger("roll");
                isroll = false;
                roll_time = 0.37f;
            }
        }

        // Hurt limit
        if (Time.time >= Hurttime && Heal > 0f)   //thoi gian toi thieu hurtbytrap 2 lan la 1s
        {
            if (IsHurtbyTrapx() || IsHurtbyTrapy())
            {
                TakeDame(25);
                Hurttime = Time.time + 0.8f;
            }
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        MovementState state;
        if (DirX > 0f && !ishurt && !ps.Inatk)
        {
            state = MovementState.running;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (DirX < 0f && !ishurt && !ps.Inatk) // lat nv neu di sang trai
        {
            state = MovementState.running;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.up;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.down;
        }

        if (Heal <= 0)
        {
            state = MovementState.die;
            rb.velocity = new Vector2(0f, -15f);
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsGround()
    {
        return Physics2D.BoxCast(transform.position, Boxsize, 0f, transform.right, checkDistance, jumpGround);
        // (vi tri, size, angle, huong di chuyen, khoang di chuyen, layer)
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.right * checkDistance, Boxsize);
    }

    bool IsHurtbyTrapy()    //kiem tra va cham voi Trap thay huong xuong
    {
        return Physics2D.BoxCast(transform.position, Boxsize, 0f, transform.right, checkDistance, Trap);
    }

    bool IsHurtbyTrapx()   //kiem tra va cham voi Trap thay huong x
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size * 1.08f, 0f, transform.up, 0.04f, Trap);
    }

    void Spawn_runEff()
    {
        Instantiate(run_effect, transform.position, transform.rotation);
    }

    void Spawn_jumpEff()
    {
        Instantiate(jump_effect, transform.position, transform.rotation);
    }
    void Spawn_rollEff()
    {
        Instantiate(roll_effect, transform.position, transform.rotation);
    }

    public void Spawn_bloodEff()
    {
        if (!ps.Inulti && !ps.Incall)
        {
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y + 1.12f);
            GameObject blood = Instantiate(BloodEff, spawnPos, transform.rotation);
            blood.transform.parent = transform;
        }
    }

    public void TakeDame(float dame)
    {
        if (!ps.Inulti && !ps.Incall)
        {
            ishurt = true;
            Heal -= dame;
            Vector2 spawnpoint = new Vector2(transform.position.x, transform.position.y + 1.8f); // spawn pop txt
            GameObject point = Instantiate(Pop_Text, spawnpoint, Quaternion.identity);
            point.transform.GetChild(0).GetComponent<TextMesh>().text = "" + dame;
            heal_bar.SetHeal(Heal);
            rb.velocity = new Vector2(0f, 0f);
            anim.SetTrigger("Ishurt");
            waterAudio.PlaySFX(waterAudio.hurt);
        }
    }

    public void TakeDame_NoAni(float dame)
    {
        if (!ps.Inulti && !ps.Incall)
        {
            Heal -= dame;
            Vector2 spawnpoint = new Vector2(transform.position.x, transform.position.y + 1.8f); // spawn pop txt
            GameObject point = Instantiate(Pop_Text, spawnpoint, Quaternion.identity);
            point.transform.GetChild(0).GetComponent<TextMesh>().text = "" + dame;
            heal_bar.SetHeal(Heal);
            rb.velocity = new Vector2(0f, 0f);
            waterAudio.PlaySFX(waterAudio.hurt);
        }
    }

    public void Death_voice()
    {
        waterAudio.PlaySFX(waterAudio.death);
    }

    public void FootStep()
    {
        waterAudio.PlaySFX(waterAudio.step);
    }

    void OutHurt()
    {
        ishurt = false;
    }
}
