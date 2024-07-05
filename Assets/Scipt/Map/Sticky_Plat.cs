using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Plat : MonoBehaviour
{
    // xet player bat dau va cham vs platform: cho platform la cha cua player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // player ket thuc va cham platform: cha cua player la null
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
