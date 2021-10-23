using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IPlayerAttack
{
    public float speed;
    int hitstun = 40;

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            EventManager.Instance.Trigger("OnFireballHit");
        }
        Destroy(gameObject);
    }

    public int GetHitstun()
    {
        return hitstun;
    }
}
