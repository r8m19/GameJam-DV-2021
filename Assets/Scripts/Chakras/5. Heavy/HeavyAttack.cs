using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour, IPlayerAttack
{
    private int hitstun = 80;
    private int damage = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            EventManager.Instance.Trigger("OnHeavyHit");
        }
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
