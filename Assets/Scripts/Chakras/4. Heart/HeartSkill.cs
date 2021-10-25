using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSkill : ChakraSkill
{
    float speedBonus = 3;

    public HeartSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
    }

    public override void Close()
    {
        base.Close();
        _player.baseSpeed -= speedBonus;
    }

    public override void Open()
    {
        base.Open();
        _player.baseSpeed += speedBonus;
    }

    public override void TryExecute()
    {
        //nothing lmao
    }
}
