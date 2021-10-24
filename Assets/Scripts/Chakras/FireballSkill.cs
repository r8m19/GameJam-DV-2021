using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : ChakraSkill
{
    GameObject fireball;

    public FireballSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        fireball = (GameObject)Resources.Load("Fireball");
        EventManager.Instance.Subscribe("OnFireballHit", OnFireballHit);
    }

    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.anim.SetTrigger("Fireball");
    }

    public void OnFireballLaunch()
    {
        Debug.Log("OnFireballLaunch");
        GameObject go = GameObject.Instantiate(fireball, _player.transform.position, Quaternion.identity);
        go.transform.right = _player.aimVector;
    }

    private void OnFireballHit(params object[] parameters)
    {
        Open();
    }
}
