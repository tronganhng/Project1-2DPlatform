using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonadoMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed, dame;

    public GameObject hitEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
            //collision.GetComponent<Enemy_HealTest>().SpawnHit_Eff();
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.GetComponent<Animator>().SetBool("explode", true);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
