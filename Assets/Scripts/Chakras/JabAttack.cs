using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabAttack : MonoBehaviour, IPlayerAttack
{
    public float speed;
    private int hitstun = 10;

    private void Start()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.position += transform.right * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            EventManager.Instance.Trigger("OnJabHit");
        }
    }

    int IPlayerAttack.GetHitstun()
    {
        return hitstun;
    }
}
