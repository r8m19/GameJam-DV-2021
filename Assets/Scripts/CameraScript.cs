using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraScript : MonoBehaviour
{
    float shakingTime;
    float startingTime;
    float startingIntensity;

    public CinemachineVirtualCamera virtualCamera;
    public static CameraScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void ScreenShake(float strength, float time)
    {
        Instance.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = strength;
        Instance.shakingTime = time;
        Instance.startingTime = time;
        Instance.startingIntensity = strength;
    }


    private void Update()
    {
        if (shakingTime > 0)
        {
            shakingTime -= Time.deltaTime;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(0, startingIntensity, shakingTime / startingTime);
        }
    }
    
}
