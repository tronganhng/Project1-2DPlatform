using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{
    public GameObject AllWall;
    public float dame;
    void Start(){
        Destroy(AllWall, 1.45f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Enemy")){
            collision.GetComponent<Enemy_HealTest>().TakeDame(dame);
        }
        if(collision.gameObject.CompareTag("Boss")){
            collision.GetComponent<Boss_Heal>().TakeDame(dame);
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.GetComponent<Animator>().SetBool("explode", true);
        }
    }
}
