using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabSkill : ChakraSkill
{
    GameObject jab1;
    GameObject jab2;
    GameObject jab3;

    public JabSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon    = _icon;
        jab1 = (GameObject)Resources.Load("Jab1");
        jab2 = (GameObject)Resources.Load("Jab2");
        jab3 = (GameObject)Resources.Load("Jab3");
        EventManager.Instance.Subscribe("OnJabHit", OnJabHit);
    }

    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.anim.SetTrigger("jab");
        _player.currentSpeed = 0;
        _player.StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        _player.currentSpeed = _player.baseSpeed;
    }

    public void OnJab1()
    {
        GameObject go = GameObject.Instantiate(jab1, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }
    public void OnJab2()
    {
        GameObject go = GameObject.Instantiate(jab2, _player.transform);
    }
    public void OnJab3()
    {
        GameObject go = GameObject.Instantiate(jab3, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }

    private void OnJabHit(params object[] parameters)
    {
        Open();
    }

}
