using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] private float dame;
    private Transform player;
    private Rigidbody2D rb;
    private Animator ani;
    private WaterMovement pm;
    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<WaterMovement>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        float x = Mathf.Abs(player.position.x - transform.position.x);
        Vector2 direct = new Vector2(transform.right.x, 0.577f).normalized; // hop voi Ox goc 30 do
        float velo = x*3.571f / Mathf.Sqrt(0.435f * x + 1.08f); // v = (x*sqrt(0.5g))/sqrt(0.435x * 1.125)

        rb.velocity = velo * direct;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ani.SetTrigger("explode2");
            rb.velocity = new Vector2(0, 0);
            if(pm.Heal > 0f) collision.GetComponent<WaterMovement>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
