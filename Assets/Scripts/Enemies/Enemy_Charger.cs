using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Charger : Enemy
{
    [Header("Atributes")]
    public float chaseSpeed;
    public float hopDistance;
    public float overshoot;
    public float attackRange;
    public float visionRange;
    
    [Header("Objects")]
    public Player target;

    [HideInInspector] public Vector2 playerDir;
    [HideInInspector] public Vector2 attackPoint;
    [HideInInspector] public Vector2 preAttackPoint;

    protected override void Awake()
    {
        base.Awake();

        if (!target)
            target = GameObject.FindObjectOfType<Player>();

        sm.AddState("Patrol", new ChargerPatrolState(sm, this));
        sm.AddState("Chase",  new ChargerChaseState(sm, this));
        sm.AddState("Attack", new ChargerAttackState(sm, this));

        sm.ChangeState("Patrol");
    }

    protected override void Update()
    {
        if (target)
            playerDir = (target.transform.position - transform.position).normalized;

        base.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.4705882f, 0.7607843f, 0.4470588f);
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

class ChargerPatrolState : IState
{
    StateMachine _sm;
    Enemy_Charger _dog;
    public ChargerPatrolState(StateMachine sm, Enemy_Charger dog)
    {
        _sm = sm;
        _dog = dog;
    }

    void IState.OnEnter()
    {

    }

    void IState.OnExit()
    {
        _dog.lastState = "Patrol";
    }

    void IState.OnUpdate()
    {
        if (Vector2.Distance(_dog.transform.position, _dog.target.transform.position) < _dog.visionRange)
        {
            Debug.Log("Player Detected");
            _sm.ChangeState("Chase");
        }
    }
}

class ChargerChaseState : IState
{
    Enemy_Charger _dog;
    StateMachine _sm;

    public ChargerChaseState(StateMachine sm, Enemy_Charger dog)
    {
        _sm = sm;
        _dog = dog;
    }

    void IState.OnEnter()
    {

    }

    void IState.OnExit()
    {
        _dog.lastState = "Chase";
    }

    void IState.OnUpdate()
    {
        _dog.transform.position += (Vector3)_dog.playerDir * _dog.chaseSpeed * Time.deltaTime;

        if (Vector2.Distance(_dog.target.transform.position, _dog.transform.position) < _dog.attackRange)
        {
            _sm.ChangeState("Attack");
        }
    }
}

class ChargerAttackState : IState
{
    Enemy_Charger _dog;
    StateMachine _sm;

    public ChargerAttackState(StateMachine sm, Enemy_Charger dog)
    {
        _sm = sm;
        _dog = dog;
    }

    void IState.OnEnter()
    {
        Debug.Log("OnAttack");
        _dog.StartCoroutine(Attack());
    }

    void IState.OnExit()
    {
        _dog.lastState = "Attack";
    }

    void IState.OnUpdate()
    {

    }

    IEnumerator Attack()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                _dog.transform.position -= (Vector3)_dog.playerDir * _dog.hopDistance;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(.3f);
        }

        _dog.attackPoint = (Vector2)_dog.target.transform.position + _dog.playerDir * _dog.overshoot;
        _dog.preAttackPoint = _dog.transform.position;

        for (float i = 0; i < 60; i++)
        {
            _dog.transform.position = Vector2.Lerp(_dog.preAttackPoint, _dog.attackPoint, i / 60);
            yield return new WaitForFixedUpdate();
        }

        _sm.ChangeState("Chase");
    }
}



