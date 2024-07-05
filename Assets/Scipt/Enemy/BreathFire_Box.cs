using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathFire_Box : MonoBehaviour
{
    [SerializeField] private float dame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<WaterMovement>().TakeDame(dame);
        }
    }
}
