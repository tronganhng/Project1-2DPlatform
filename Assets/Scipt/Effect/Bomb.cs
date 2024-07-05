using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public LayerMask Ground, Trap;
    public float checkDistance, gravity_scale;
    public Vector2 Boxsize;
    public float dame;

    private Animator ani;
    private Transform player;
    private Rigidbody2D rb;
    private EnemyAudio audioManager;
    CameraShake mycamera;

    void Start()
    {
        mycamera = FindObjectOfType<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        audioManager = FindObjectOfType<EnemyAudio>();
        // add luc
        Vector2 direct = new Vector2(transform.right.x, 1.192f).normalized;
        float x = Mathf.Abs(player.position.x - transform.position.x);
        float velo = (1.556f*x*Mathf.Sqrt(9.81f*gravity_scale)) / Mathf.Sqrt(0.5f + 2.383f * x);  // v = 1,56x.sqrt(g)/sqrt(1+2,38x)
        rb.velocity = direct * velo;
    }

    void Update()
    {
        if ((IsGround()||IsTrap()) && !ani.GetBool("explode"))
        {
            audioManager.PlaySFX(audioManager.explode);
            ani.SetBool("explode", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            collision.GetComponent<WaterMovement>().TakeDame(dame);
        }
    }

    public bool IsGround()
    {
        return Physics2D.BoxCast(transform.position, Boxsize, 0f, transform.up, checkDistance, Ground);

        // (vi tri, size, angle, huong di chuyen, khoang di chuyen, layer)
    }

    public bool IsTrap()
    {
        return Physics2D.BoxCast(transform.position, Boxsize, 0f, transform.up, checkDistance, Trap);

        // (vi tri, size, angle, huong di chuyen, khoang di chuyen, layer)
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.up * checkDistance, Boxsize);
    }

    void ShakeCam()
    {
        if(mycamera != null)
            mycamera.Shake(2.6f, 0.22f);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
