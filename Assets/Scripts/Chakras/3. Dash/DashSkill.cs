using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : ChakraSkill, IPlayerAttack
{
    int hitstun = 80;
    int damage = 5;

    float range = 5;
    Vector2 endpoint;
    Afterimage aftimg;

    public DashSkill(Player player, ChakraIcon _icon)
    {
        _player = player;
        icon = _icon;
        EventManager.Instance.Subscribe("OnDashHit", OnDashHit);
        aftimg = Resources.Load<Afterimage>("Afterimage");
    }

    protected override void Execute()
    {
        base.Execute();
        Close();
        _player.StartCoroutine(_player.Invencibility(15));

        List<Enemy> hitTargets = HitTargets(Physics2D.RaycastAll(_player.transform.position, _player.aimVector, range));

        foreach (Enemy enem in hitTargets)
        {
            enem.OnEnemyHit(this);
        }
        if (hitTargets.Count > 0)
            Open();

        Afterimage go = GameObject.Instantiate<Afterimage>(aftimg);
        go.Display(endpoint, _player.transform.position - (Vector3)_player.aimVector * _player.speed * Time.deltaTime * 60);

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
        Open();
    }

    public PlayerHit GetPlayerHit()
    {
        return new PlayerHit(damage, hitstun, _player.transform.position);
    }
}
