using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open : MonoBehaviour
{
    [SerializeField] private Transform Enemy_cnt;
    private Rigidbody2D rb;
    public int childleft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if(Enemy_cnt.childCount <= childleft)
        {
            rb.velocity = new Vector2(0, 3);
            Invoke("DestroyDoor", 2f);
        }        
    }

    void DestroyDoor()
    {
        Destroy(gameObject);
    }
}
