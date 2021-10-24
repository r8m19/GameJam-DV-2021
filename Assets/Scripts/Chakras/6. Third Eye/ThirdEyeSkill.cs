using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEyeSkill : ChakraSkill
{
    GameObject thirdEye;

    public ThirdEyeSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        thirdEye = Resources.Load<GameObject>("ThirdEye");
    }

    protected override void Execute()
    {
        base.Execute();
        Close();

        GameObject.Instantiate(thirdEye, _player.transform.position, Quaternion.identity);        
    }

}
