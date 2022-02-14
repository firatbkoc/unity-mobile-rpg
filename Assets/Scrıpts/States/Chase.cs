using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    private readonly Enemy _enemy;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public Chase(Enemy enemy, NavMeshAgent navMeshAgent, Animator animator)
    {
        _enemy = enemy;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick() 
    {
        _navMeshAgent.SetDestination(_enemy.Target.transform.position);
    }
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        
        _animator.SetFloat(Speed, 1f);
    }

    public void OnExit() 
    {
        _navMeshAgent.enabled = false;
        _animator.SetFloat(Speed, 0f);
    }

    public override string ToString()
    {
        return "chase";
    }
}
