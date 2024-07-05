using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAtk_hitBox : MonoBehaviour
{
    [SerializeField] private float dame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
        }
    }
}
