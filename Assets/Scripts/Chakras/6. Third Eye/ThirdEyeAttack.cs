using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ThirdEyeAttack : MonoBehaviour, IPlayerAttack
{
    public int hitstun = 2;
    public int damage = 999;
    public float speed;

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
        _light.pointLightOuterRadius += 1.6f * speed * expanding * Time.deltaTime;
        _light.pointLightInnerRadius += 1f * speed * expanding * Time.deltaTime;
        _light.intensity += 1f * speed * expanding * Time.deltaTime;

        if (_light.pointLightOuterRadius >= 25)
        {
            if (!killed)
            {
                foreach (Enemy item in GameObject.FindObjectsOfType<Enemy>())
                {
                    if (item.GetComponent<Renderer>().isVisible)
                    {
                        Debug.Log("about to kill " + item);
                        item.OnEnemyHit(this);
                    }
                }
                killed = true;
            }
            expanding = -16;
        }
        if (_light.pointLightOuterRadius < 0)
        {
            Destroy(gameObject);
        }


    }
}
