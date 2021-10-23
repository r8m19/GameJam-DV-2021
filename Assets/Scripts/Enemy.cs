using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected StateMachine sm;

    [HideInInspector] public string lastState;
    [HideInInspector] public int currentHitstun = 0;
    [HideInInspector] public Vector2 hitPoint;

    protected virtual void Awake()
    {
        sm = new StateMachine();
        sm.AddState("Hitstun", new HitstunState(sm, this));   
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
        currentHitstun = collision.GetComponent<IPlayerAttack>().GetHitstun();
        hitPoint = collision.gameObject.transform.position;
        sm.ChangeState("Hitstun");
    }
    public void OnEnemyHit(IPlayerAttack attacker, Vector3 attackPosition)
    {
        currentHitstun = attacker.GetHitstun();
        hitPoint = attackPosition;
        sm.ChangeState("Hitstun");
    }
}

class HitstunState : IState
{
    StateMachine _sm;
    Enemy _enemy;
    int _histstun;

    public HitstunState(StateMachine sm, Enemy enemy)
    {
        _enemy = enemy;
        _sm = sm;
    }

    void IState.OnEnter()
    {
        _histstun = _enemy.currentHitstun;
        _enemy.StartCoroutine(TakePushback());
        _enemy.StartCoroutine(TakeHitstun());
    }

    void IState.OnExit()
    {
        _enemy.lastState = "Hitstun";
    }

    void IState.OnUpdate()
    {

    }

    IEnumerator TakeHitstun()
    {
        for (int i = 0; i < _histstun; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        _sm.ChangeState(_enemy.lastState);
    }

    IEnumerator TakePushback()
    {
        for (int i = 0; i < _histstun/3; i++)
        {
            _enemy.transform.position -= ((Vector3)_enemy.hitPoint - _enemy.transform.position).normalized * Time.deltaTime * 3;
            yield return new WaitForFixedUpdate();
        }

    }
}
