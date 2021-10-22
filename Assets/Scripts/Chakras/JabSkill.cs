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
        open = false;
        icon.UpdateImage(open);
        _player.anim.SetTrigger("Jab");
    }

    public void OnJabStart()
    {
        Debug.Log("OnJabStart");
        GameObject go = GameObject.Instantiate(jabAttack, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
        //Debug.Log("Jab Skill executed correctly!");
    }

    private void OnJabHit(params object[] parameters)
    {
        open = true;
        icon.UpdateImage(open);
    }

}
