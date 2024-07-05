using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Eff : MonoBehaviour
{
    private Animator ani;
    public float active_time;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if(active_time <= 0f)
        {
            ani.SetBool("off", true);
        }
        active_time -= Time.deltaTime;
    }

    void DestroyEff()
    {
        if (transform.parent)
        {
            transform.parent.GetComponent<Animator>().SetBool("freeze", false);
        }
        Destroy(gameObject);
    }
}
