using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownSkill : ChakraSkill
{
    GameObject crownAttack;
    public CrownSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        crownAttack = (GameObject)Resources.Load("Crown");
    }

    protected override void Execute()
    {
        base.Execute();
        Close();
        GameObject go = GameObject.Instantiate(crownAttack, _player.transform.position - 1f * Vector3.up, Quaternion.identity);
        go.transform.parent = _player.transform;
    }
    
    void OnCrownHit()
    {
        Open();
    }

}
