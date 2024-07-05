using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera cinemachine;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform a = player.GetComponentInChildren<Transform>(true).Find("Move point"); // tim move point la con cua player
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        cinemachine.Follow = a;
    }


}
