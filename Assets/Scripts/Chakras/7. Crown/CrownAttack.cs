using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownAttack : MonoBehaviour, IPlayerAttack
{
    private int hitstun = 40;
    private int damage = 10;


    private void OnAnimationEnd()
    {
        Destroy(gameObject);   
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, transform.position);
    }
}
