using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour // movement should be seperated
{
    //ondeath event maybe
    public event Action OnPlayerDeath;
    public float nextAttackTime;
    public float attackPerSecond = 2f;
    public int attackDamage = 1;
    public int defense;
    public float healthPoints = 15f;
    public bool isDead = false;

    public float runSpeed = 7f;
    public float walkSpeed = 4f;
    public float rotationSpeed= 720;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    protected Joystick joystick;
    private Animator animator;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        animator = GetComponentInChildren<Animator>();
        inventory = Inventory.instance;
        inventory.OnInventoryChange += ModifyStats;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void Damage(float amount) // animation, death event
    {
        amount = amount - defense;
        if (amount < 0)
            amount = 0;
        healthPoints -= amount;
        Debug.Log("player takes " + amount + " damage");

        if (healthPoints <= 0)
        {
            isDead = true;
            OnPlayerDeath?.Invoke();
            return;
        }
    }

    public void Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + (1f / attackPerSecond);
            //Debug.Log("player attack!");
            animator.SetTrigger("Attack");

            Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider collider in hitColliders)
            {
                //Debug.Log("hit" + collider.name);
                
                collider.GetComponent<Enemy>().Damage(attackDamage);
            }
            //_enemy.Target.Damage(3f);
        }
    }

    void ModifyStats()
    {
        attackDamage += inventory.attackMods;
        defense += inventory.defenseMods;
    }

    private void MovePlayer()
    {
        if (isDead)
            return;
        Vector3 inputVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        inputVector.Normalize();

        if (Mathf.Abs(joystick.Horizontal) > 0.5f || Mathf.Abs(joystick.Vertical) > 0.5f)
            transform.Translate(inputVector * runSpeed * Time.deltaTime, Space.World);
        else
            transform.Translate(inputVector * walkSpeed * Time.deltaTime, Space.World);

        if (inputVector != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
            Quaternion toRotation = Quaternion.LookRotation(inputVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
