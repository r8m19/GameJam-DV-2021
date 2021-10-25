using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabSkill : ChakraSkill
{
    GameObject jabAttack;

    public JabSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon    = _icon;
        jabAttack = (GameObject)Resources.Load("Jab");
        EventManager.Instance.Subscribe("OnJabHit", OnJabHit);
    }

    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.anim.SetTrigger("jab");
    }

    public void OnJab1()
    {
        GameObject go = GameObject.Instantiate(jabAttack, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }
    public void OnJab2()
    {
        GameObject go = GameObject.Instantiate(jabAttack, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }
    public void OnJab3()
    {
        GameObject go = GameObject.Instantiate(jabAttack, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }

    private void OnJabHit(params object[] parameters)
    {
        Open();
    }

}
