using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EarthBump : MonoBehaviour
{
    public float dame, power;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.45f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Enemy")){
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.right.x * 1.5f, 3f) * power;
            collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
        }
        if(collision.gameObject.CompareTag("Boss")){
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
        }
    }
}
