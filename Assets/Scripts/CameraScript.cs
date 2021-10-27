using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float shakingTime;

    /*
    public static void ScreenShake(float strength, float time)
    {
        Instance.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = strength;
        Instance.shakingTime = time;
        Instance.startingTime = time;
        Instance.startinIntensity = strength;
    }


    private void Update()
    {
        if (shakingTime > 0)
        {
            shakingTime -= Time.deltaTime;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(0, startinIntensity, shakingTime / startingTime);
        }
    }
    */
}
