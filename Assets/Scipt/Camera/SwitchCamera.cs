using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera1, Camera2;
    public int Manager;
    public bool changed_cam = false;

    public void CameraManager() // Change Camera
    {
        if(Manager == 0)
        {
            Cam_2();
            changed_cam = true;
            Manager = 1;
        }
        else
        {
            Cam_1();
            changed_cam = true;
            Manager = 0;
        }
    }

    void Cam_1() // Bat cam 1
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    void Cam_2() // Bat cam 2
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
}
