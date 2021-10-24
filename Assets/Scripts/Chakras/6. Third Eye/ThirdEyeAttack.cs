using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ThirdEyeAttack : MonoBehaviour, IPlayerAttack
{
    public int hitstun = 20;
    public int damage = 999;

    Light2D _light;
    int expanding = 1;
    bool killed = false;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, transform.position);
    }

    private void Update()
    {
        _light.pointLightOuterRadius += 0.016f * expanding;
        _light.pointLightInnerRadius += 0.01f * expanding;
        _light.intensity += 0.01f * expanding;

        if (_light.pointLightOuterRadius >= 23)
        {
            if (!killed)
            {
                foreach (Enemy item in GameObject.FindObjectsOfType<Enemy>())
                {
                    Debug.Log("about to kill " + item);
                    item.OnEnemyHit(this);
                }
                killed = true;
            }
            expanding = -8;
        }
        if (_light.pointLightOuterRadius < 0)
        {
            Destroy(gameObject);
        }


    }
}
