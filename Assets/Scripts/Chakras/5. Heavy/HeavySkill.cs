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
        EventManager.Instance.Subscribe("OnHeavyHit", OnHeavyHit);
    }
    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.anim.SetTrigger("Heavy");
    }

    public void OnHeavyStart()
    {
        GameObject.Instantiate(heavyAttack, _player.transform.position, Quaternion.identity);
    }

    private void OnHeavyHit(params object[] parameters)
    {
        Open();
    }
}
