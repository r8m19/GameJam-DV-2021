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

    void OnClose()
    {
        _player.speed -= speedBonus;
    }

    void OnOpen()
    {
        _player.speed += speedBonus;
    }

    public override void TryExecute()
    {
        //nothing lmao
    }
}
