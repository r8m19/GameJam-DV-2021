using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSkill : ChakraSkill
{
    float speedBonus;

    public HeartSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
    }

    public override void Close()
    {
        base.Close();
        _player.speed -= speedBonus;
    }

    public override void Open()
    {
        base.Open();
        _player.speed += speedBonus;
    }

    public override void TryExecute()
    {
        //nothing lmao
    }
}
