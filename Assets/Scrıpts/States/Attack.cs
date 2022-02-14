using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private readonly Enemy enemy;
    public float nextAttackTime;
    public float attackPerSecond = 1f;
    public float rotationSpeed = 2f;
    private readonly Animator animator;

    //private Coroutine lookCoroutine;
    public Attack(Enemy _enemy, Animator _animator)
    {
        enemy = _enemy;
        animator = _animator;
    }
    public void Tick()
    {
        if (enemy.Target != null)
        {
            RotateTowardsTarget();

            if (nextAttackTime <= Time.time)
            {
                nextAttackTime = Time.time + (1f / attackPerSecond);
                //Debug.Log("enemy attack!");
                //enemy.transform.LookAt(enemy.Target.transform);
                enemy.Target.Damage(enemy.attackDamage);
                //_enemy.TakeFromTarget();
                animator.SetTrigger("Attack");
            }
        }
    }

    public void RotateTowardsTarget()
    {
        Vector3 direction = enemy.Target.transform.position - enemy.transform.position;

        Quaternion toRotation = Quaternion.LookRotation(direction);
        Quaternion toRotationY = Quaternion.Euler(enemy.transform.rotation.eulerAngles.x, toRotation.eulerAngles.y, enemy.transform.rotation.eulerAngles.z);

        enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, toRotationY, 720 * Time.deltaTime);    
    }
    public void OnEnter() 
    {

    }
    public void OnExit() { }
}
