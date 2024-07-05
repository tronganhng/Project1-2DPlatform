using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Eff : MonoBehaviour
{
    CameraShake mycamera;
    private void Start()
    {
        mycamera = FindObjectOfType<CameraShake>();
    }

    void ShakeCam()
    {
        if(mycamera != null)
            mycamera.Shake(2.1f, 0.17f);
    }

    void Destroy_Effect()
    {
        Destroy(gameObject);
    }
}
