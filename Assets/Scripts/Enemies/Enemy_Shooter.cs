using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{
    public float fireRate;
    public float visionRange;

    public Player target;
    public GameObject fireball;

    protected override void Awake()
    {
        base.Awake();

        if (!target)
            target = GameObject.FindObjectOfType<Player>();
        if (!fireball)
            fireball = Resources.Load<GameObject>("EnemyFireball");

        sm.AddState("Sleep", new ShooterSleepState(sm, this));
        sm.AddState("Shoot", new ShooterShootingState(sm, this));

        sm.ChangeState("Sleep");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.4705882f, 0.7607843f, 0.4470588f); //unity collider green
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

}

class ShooterSleepState : IState
{
    StateMachine _sm;
    Enemy_Shooter _dog;
    public ShooterSleepState(StateMachine sm, Enemy_Shooter dog)
    {
        _dog = dog;
        _sm = sm;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (Vector3.Distance(_dog.transform.position, _dog.target.transform.position) < _dog.visionRange)
        {
            _sm.ChangeState("Shoot");
        }
    }
}

class ShooterShootingState : IState
{
    StateMachine _sm;
    Enemy_Shooter _dog;
    public ShooterShootingState(StateMachine sm, Enemy_Shooter dog)
    {
        _dog = dog;
        _sm = sm;
    }

    public void OnEnter()
    {
        Debug.Log("OnEnter");
    }

    public void OnExit()
    {

    }

    float next;
    public void OnUpdate()
    {
        if (next <= Time.time)
        {
            next = Time.time + _dog.fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject go = GameObject.Instantiate(_dog.fireball, _dog.transform.position, Quaternion.identity);
        go.transform.right = (_dog.target.transform.position - _dog.transform.position).normalized;
    }
}