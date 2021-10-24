using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{
    public float fireRate;
    public float visionRange;

    public Player target;

    protected virtual void Awake()
    {
        base.Awake();

        if (!target)
            target = GameObject.FindObjectOfType<Player>();

        sm.AddState("Sleep", new ShooterSleepState(sm, this));
        sm.AddState("Chase", new ShooterShootingState(sm, this));

        sm.ChangeState("Patrol");
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

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}