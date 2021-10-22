using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabSkill : ChakraSkill
{
    public JabSkill(Player player)
    {
        _player = player;
    }

    protected override void Execute()
    {
        base.Execute();
        open = false;
        _player.anim.SetTrigger("Jab");
    }

    public void OnJabStart()
    {
        Debug.Log("Jab Skill executed correctly!");
    }

}
