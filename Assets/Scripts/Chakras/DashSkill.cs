using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : ChakraSkill
{
    //GameObject fireball;
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
        //open = false;
        icon.UpdateImage(open);

        RaycastHit2D[] line = HitTargets(Physics2D.RaycastAll(_player.transform.position, _player.aimVector, range, 8));

        _player.transform.position = endpoint;
    }

    RaycastHit2D[] HitTargets(RaycastHit2D[] _line)
    {
        bool collided = false;
        int i = 0;
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        if (_line.Length > 0)
        {
            while (!collided || i < _line.Length)
            {
                Debug.Log(_line[i].collider);
                if (_line[i].collider.gameObject.layer == 10)
                {
                    Debug.Log(_line[i].point);

                    collided = true;
                    endpoint = _line[i].point;
                }

                if (_line[i].collider.gameObject.layer == 9)
                {
                    hits.Add(_line[i]);
                }

                i++;
            }
        }

        if (collided == false)
        {
            endpoint = (Vector2)_player.transform.position + _player.aimVector * range;
        }

        return hits.ToArray();
    }

    public void OnDash() //Animation callback
    {
        
    }

    private void OnDashHit(params object[] parameters)
    {
        open = true;
        icon.UpdateImage(open);
    }
}
