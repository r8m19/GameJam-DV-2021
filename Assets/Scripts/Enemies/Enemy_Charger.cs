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

    ChargerAttackState attackState;

    protected override void Awake()
    {
        base.Awake();

        if (!target)
            target = GameObject.FindObjectOfType<Player>();

        attackState = new ChargerAttackState(sm, this);

        sm.AddState("Patrol", new ChargerPatrolState(sm, this));
        sm.AddState("Chase",  new ChargerChaseState(sm, this));
        sm.AddState("Attack", attackState);

        sm.ChangeState("Patrol");
    }

    protected override void Update()
    {
        playerDir = (target.transform.position - transform.position).normalized;

        base.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.4705882f, 0.7607843f, 0.4470588f); //unity collider green
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnAttackStart()
    {
        attackState.OnAttackStart();   
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
        _dog.transform.right = new Vector2(_dog.playerDir.x, 0);

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
    private bool attacking = false;

    public ChargerAttackState(StateMachine sm, Enemy_Charger dog)
    {
        _sm = sm;
        _dog = dog;
    }

    void IState.OnEnter()
    {
        _dog.anim.SetTrigger("attack");
        attacking = false;
    }

    void IState.OnExit()
    {
        _dog.anim.SetTrigger("land");
        _dog.lastState = "Attack";
    }

    void IState.OnUpdate()
    {
        if(!attacking)
        _dog.transform.right = new Vector2(_dog.playerDir.x, 0);
    }

    public void OnAttackStart(params object[] parameters)
    {
        _dog.StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        attacking = true;
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



