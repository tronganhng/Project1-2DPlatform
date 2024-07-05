using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public float speed, dame;
    private bool istouch;
    private Animator ani;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        if (!istouch) rb.velocity = transform.right * speed;
        else rb.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            istouch = true;
            ani.SetTrigger("explode");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            istouch = true;
            ani.SetTrigger("explode");
            collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            istouch = true;
            ani.SetTrigger("explode");
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            istouch = true;
            ani.SetTrigger("explode");
            collision.GetComponent<Animator>().SetBool("explode", true);
        }
    }

    void DestroyEff()
    {
        Destroy(gameObject);
    }
}
