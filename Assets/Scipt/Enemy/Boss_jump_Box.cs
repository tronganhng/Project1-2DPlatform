using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_jump_Box : MonoBehaviour
{
    [SerializeField] private float dame, power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<WaterMovement>().TakeDame(dame);
            Vector3 direct = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(direct.x * 2.5f * power, 4f * power);
        }       
    }
}
