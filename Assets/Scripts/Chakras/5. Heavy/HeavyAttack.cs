using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour, IPlayerAttack
{
    private int hitstun = 120;
    private int damage = 120;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);   
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, transform.position);
    }
}
