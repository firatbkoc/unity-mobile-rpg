using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float healthPoints = 15f;
    public bool isDead = false;
    public float attackRange = 1f;
    public float attackDamage = 3f;
    public float sightRadius = 10f;
    private StateMachine stateMachine;
    private Animator animator;
    public Player Target { get; set; }
    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();

        var roamState = new Roam(this);
        var chaseState = new Chase(this, navMeshAgent, animator);
        var attackState = new Attack(this, animator);
        var deadState = new Dead(this, animator);

        void AddTransition(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
        void AddPrioTransition(IState to, Func<bool> condition) => stateMachine.AddPrioTransition(to, condition);

        AddPrioTransition(deadState, IsDead());
        AddTransition(roamState, chaseState, HasTarget());
        AddTransition(chaseState, roamState, TargetIsDead());
        AddTransition(chaseState, attackState, TargetInRange());
        AddTransition(attackState, chaseState, TargetOutOfRange());
        AddTransition(attackState, roamState, TargetIsDead());

        stateMachine.SetState(roamState);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> TargetIsDead() => () => Target.isDead == true;
        Func<bool> IsDead() => () => (isDead == true);
        Func<bool> TargetInRange() => () => Vector3.Distance(Target.transform.position, transform.position) < attackRange;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(Target.transform.position, transform.position) > attackRange;

    }

    public void Damage(float amount) 
    {
        if (isDead)
            return;

        Debug.Log("enemy takes " + amount + " damage");
        healthPoints -= amount;
        if (healthPoints <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            //Die();
            Destroy(gameObject, 8f);
            return;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
        //Debug.Log(stateMachine.ToString());
    }
}
