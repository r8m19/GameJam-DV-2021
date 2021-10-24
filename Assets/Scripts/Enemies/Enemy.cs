using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    protected StateMachine sm;
    private   GameObject deathParticles;

    [HideInInspector] public string lastState;
    [HideInInspector] public PlayerHit lastHit;

    protected virtual void Awake()
    {
        sm = new StateMachine();
        sm.AddState("Hitstun", new HitstunState(sm, this));

        deathParticles = Resources.Load<GameObject>("DeathParticles");
        currentHP = maxHP;
    }

    protected virtual void Update()
    {
        sm.OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnEnemyHit(collision);
        }
    }

    public void OnEnemyHit(Collider2D collision)
    {
        lastHit = collision.GetComponent<IPlayerAttack>().GetPlayerHit();
        sm.ChangeState("Hitstun");
    }
    public void OnEnemyHit(IPlayerAttack attacker)
    {
        lastHit = attacker.GetPlayerHit();
        sm.ChangeState("Hitstun");
    }

    public void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

class HitstunState : IState
{
    StateMachine _sm;
    Enemy _enemy;
    PlayerHit hit;

    public HitstunState(StateMachine sm, Enemy enemy)
    {
        _enemy = enemy;
        _sm = sm;
    }

    void IState.OnEnter()
    {
        hit = _enemy.lastHit;
        Debug.Log("Entered hitstun from " + hit + " damage: " + hit.damage + " histun: " + hit.hitstun);

        _enemy.currentHP -= hit.damage;
        _enemy.StopCoroutine(TakePushback());
        _enemy.StopCoroutine(TakeHitstun());

        _enemy.StartCoroutine(TakePushback());
        _enemy.StartCoroutine(TakeHitstun());
    }

    void IState.OnExit()
    {

    }

    void IState.OnUpdate()
    {

    }

    IEnumerator TakeHitstun()
    {
        for (int i = 0; i < hit.hitstun; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        _sm.ChangeState(_enemy.lastState);

        if (_enemy.currentHP <= 0)
        {
            _enemy.Die();
        }
    }

    IEnumerator TakePushback()
    {
        for (int i = 0; i < hit.hitstun/3; i++)
        {
            _enemy.transform.position -= ((Vector3)hit.attackPos - _enemy.transform.position).normalized * Time.deltaTime * 3;
            yield return new WaitForFixedUpdate();
        }

    }
}

public class PlayerHit //A hit that landed from the player onto the enemy
{
    public int damage, hitstun;
    public Vector2 attackPos;

    public PlayerHit(int _damage, int _hitstun, Vector2 _attackPos)
    {
        damage = _damage;
        hitstun = _hitstun;
        attackPos = _attackPos;
    }
}