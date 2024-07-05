using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Explode : MonoBehaviour
{
    [SerializeField] private float dame, range, power_push;
    [SerializeField] private LayerMask playerLayer, boxLayer;
    [SerializeField] private Transform atk_point;
    private Rigidbody2D player;
    private EnemyAudio audioManager;
    CameraShake mycamera;

    private void Start()
    {
        mycamera = FindObjectOfType<CameraShake>();
        audioManager = FindObjectOfType<EnemyAudio>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Explode()
    {
        audioManager.PlaySFX(audioManager.explode2);
        // tao vong tron quet player
        Collider2D[] explode = Physics2D.OverlapCircleAll(atk_point.position, range);
        //dame
        foreach(Collider2D character in explode)
        {
            if(((1 << character.gameObject.layer) & playerLayer) != 0)
            {
                character.GetComponent<WaterMovement>().TakeDame(dame);
                int direct;
                if (player.position.x <= atk_point.position.x) direct = -1;
                else direct = 1;
                player.velocity = new Vector2(2f * direct, 2f) * power_push;
            }
            if(((1 << character.gameObject.layer) & boxLayer) != 0)
            {
                character.GetComponent<Animator>().SetBool("explode", true);
            }
        }
    }

    private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(atk_point.position, range); 
    }

    void ShakeCam()
    {
        if(mycamera != null)
            mycamera.Shake(3.5f, 0.3f);
    }
}
