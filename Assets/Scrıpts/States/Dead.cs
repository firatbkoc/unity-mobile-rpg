using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IState
{
    private readonly Enemy enemy;
    private readonly Animator animator;
    public Dead(Enemy _enemy, Animator _animator)
    {
        enemy = _enemy;
        animator = _animator;
    }
    public void Tick()
    {

    }
    public void OnEnter()
    {
        Debug.Log("enemy entered dead state");
        //disable behaviour
        //enemy.GetComponent<Enemy>().enabled = false;
    }
    public void OnExit() { }
}
