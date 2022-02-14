using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roam : IState
{
    private readonly Enemy _enemy;
    
    public Roam(Enemy enemy)
    {
        _enemy = enemy;
    }
    public void Tick() 
    {
        MoveInRandomDirection();

        _enemy.Target = LookForTarget();
    }
    public Player LookForTarget()
    {
        Player playerobj = Object.FindObjectOfType<Player>();
        if (Vector3.Distance(playerobj.transform.position, _enemy.transform.position) <= _enemy.sightRadius)
            return playerobj;
        return null;
    }
    public void MoveInRandomDirection() { }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_enemy.transform.position, _enemy.sightRadius);
    }
    public void OnEnter()
    {

    }

    public void OnExit() { }

    public override string ToString()
    {
        return "roam";
    }
}
