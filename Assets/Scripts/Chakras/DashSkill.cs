using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : ChakraSkill, IPlayerAttack
{
    int hitstun = 120;
    float range = 5;
    Vector2 endpoint;
    

    public DashSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        EventManager.Instance.Subscribe("OnDashHit", OnDashHit);
    }

    protected override void Execute()
    {
        base.Execute();
        open = false;
        icon.UpdateImage(open);

        List<Enemy> hitTargets = HitTargets(Physics2D.RaycastAll(_player.transform.position, _player.aimVector, range));

        foreach (Enemy enem in hitTargets)
        {
            Debug.Log("hit lol 2");
            enem.OnEnemyHit(this, _player.transform.position);
        }

        _player.transform.position = endpoint;
    }

    List<Enemy> HitTargets(RaycastHit2D[] _line)
    {
        bool collided = false;
        int i = 0;
        List<Enemy> hits = new List<Enemy>();

        if (_line.Length > 0)
        {
            while (!collided && i < _line.Length)
            {
                if (_line[i].collider.gameObject.layer == 10) //Terrain
                {
                    collided = true;
                    endpoint = _line[i].point;
                }

                if (_line[i].collider.gameObject.layer == 9) //Enemy
                {
                    Debug.Log("hit lol 1");
                    hits.Add(_line[i].collider.GetComponent<Enemy>());
                }

                i++;
            }
        }

        if (collided == false)
        {
            endpoint = (Vector2)_player.transform.position + _player.aimVector * range;
        }

        return hits;
    }

    public void OnDash() //Animation callback
    {
        
    }

    private void OnDashHit(params object[] parameters)
    {
        open = true;
        icon.UpdateImage(open);
    }

    public int GetHitstun()
    {
        return hitstun;
    }
}
