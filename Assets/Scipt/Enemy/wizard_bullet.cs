using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard_bullet : MonoBehaviour
{
    [SerializeField] private float dame, speed, time_cooldown;
    private Transform player;
    private Rigidbody2D rb;
    private Animator ani;
    private WaterMovement pm;
    private void Awake()
    {
        pm = FindObjectOfType<WaterMovement>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Vector3 target = new Vector3(player.position.x, player.position.y + 1.37f, 0f);
        Vector2 direct = (target - transform.position).normalized; // tao vector do dai 1 huong tu atk_point den nv
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb.velocity = speed * direct;
    }

    private void Update()
    {
        time_cooldown -= Time.deltaTime;
        if (time_cooldown <= 0f)
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pm.isroll)
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
            if(pm.Heal > 0f) 
            {
                if(collision.GetComponent<Player_State>().Inatk)    // chỉ chạy anim khi ko atk
                {
                    collision.GetComponent<WaterMovement>().TakeDame_NoAni(dame);
                }
                else collision.GetComponent<WaterMovement>().TakeDame(dame);
            }
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
        }
    }

    void DestroyBlluet()
    {
        Destroy(gameObject);
    }
}
