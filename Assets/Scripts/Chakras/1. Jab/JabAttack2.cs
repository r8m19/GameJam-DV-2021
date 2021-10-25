using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabAttack2 : MonoBehaviour, IPlayerAttack
{
    private int hitstun = 10;
    private int damage = 10;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            EventManager.Instance.Trigger("OnJabHit");
        }
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, transform.position);
    }
}
