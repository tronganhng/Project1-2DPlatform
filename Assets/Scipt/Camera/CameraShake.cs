using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private float shakeTimer;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float magnitude, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = magnitude;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer < 0f)
        {
            CinemachineBasicMultiChannelPerlin perlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = 0f;
        }
        shakeTimer -= Time.deltaTime;
    }
}
