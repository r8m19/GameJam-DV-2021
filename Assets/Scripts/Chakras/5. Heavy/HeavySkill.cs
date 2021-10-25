using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySkill : ChakraSkill
{
    GameObject heavyAttack;
    public HeavySkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        heavyAttack = Resources.Load<GameObject>("Heavy");
    }
    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.anim.SetTrigger("Heavy");
    }

    public void OnHeavyStart()
    {
        //_player.speed = _player.speed * 0.2f;
    }

    public void OnHeavyExplode()
    {
        GameObject go = GameObject.Instantiate(heavyAttack, _player.transform.position, Quaternion.identity);
    }

    public void OnHeavyEnd()
    {
        //_player.speed = _player.speed * 5f;
    }

    private void OnHeavyHit(params object[] parameters)
    {
        Open();
    }
}
