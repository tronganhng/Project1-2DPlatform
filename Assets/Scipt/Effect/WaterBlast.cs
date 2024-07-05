using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlast : MonoBehaviour
{
    public float speed, dame;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy_HealTest>().currentHeal > 0f)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.right.x * 2.5f, 6f);
                collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (collision.GetComponent<Animator>().GetBool("freeze")) collision.GetComponent<Boss_Heal>().SpawnHit_Eff();
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.GetComponent<Animator>().SetBool("explode", true);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.GetComponent<Animator>().SetBool("off", true);
            rb.velocity = new Vector2(0, 0);
        }
    }

    void DestroyEff()
    {
        Destroy(gameObject);
    }
}
