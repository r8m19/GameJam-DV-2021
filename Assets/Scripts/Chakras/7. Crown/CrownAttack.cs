using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownAttack : MonoBehaviour, IPlayerAttack
{
    private int hitstun = 15;
    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);   
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, transform.position);
    }
}
