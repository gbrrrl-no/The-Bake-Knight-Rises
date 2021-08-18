using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    private enum State {
        Alive,
        Dead
    }

    #region Public Variables
    [Header("Combat Variables")]
    public float attackDistance; //Min dist for attack
    public float moveSpeed;
    public float cooldownTimer; // Cooldown between attacks
    public int curHealth;
    public int maxHealth = 100;

    [Header("Combat Objects")]
    public HealthBar healthBar;
    public Transform leftLimit;
    public Transform rightLimit;
    public GameObject hotzone;
    public GameObject triggerArea;
    public Vector3 attackOffset;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool isPlayerInRange;
    [Header("Loot Table")]
    public LootTable lootSystem;
    #endregion

    #region Private Variables
    private Rigidbody2D rb;
    private Animator anim;
    private float distance;
    private float intTimer;
    private bool isAttacking;
    private bool isOnCooldown;
    private bool isOnAttackRange;
    private State state;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = cooldownTimer;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isDead") == false)
        {   
            if(!isAttacking && !isOnAttackRange)
            {
                Move();
            }

            if(!InsideOfLimits() && !isPlayerInRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Pig_Attack"))
            {
                SelectTarget();
            }

            if (isOnCooldown)
            {
                Cooldown();
                anim.SetBool("Attack", false);
            }


            if(isPlayerInRange)
            {
                EnemyLogic();
            }
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position + attackOffset, target.position);
        if(distance > attackDistance)
        {
            StopAttack();
        } else if(!isOnCooldown)
        {
            Attack();
        }
    }
    
    void Move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Pig_Attack"))
        {
            anim.SetBool("canWalk", true);
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
        cooldownTimer = intTimer; //Reset timer when Player enter Attack Ranger
        isAttacking = true;
        isOnAttackRange = true;
    }

    void StopAttack()
    {
        isAttacking = false;
        isOnAttackRange = false;
        anim.SetBool("Attack", false);
    }

    void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            isOnCooldown = false;
            cooldownTimer = intTimer;
        }
    }

    public void TriggerCooling()
    {
        isOnCooldown = true;
    }

    public void TakeDamage(int dmg)
    {
        anim.SetBool("isBeingHit", true);
        curHealth -= dmg;
        curHealth = Mathf.Max(curHealth, 0);
        healthBar.SetHealth(curHealth);
        if (curHealth == 0 && state == State.Alive){
            HandleDeath();       
        }
    }

    public void HandleDeath()
    {
        state = State.Dead;
        anim.SetBool("isDead", true);
        healthBar.SetVisible(false);
        DropLoot();
        Destroy(this.gameObject);
    }

    private void DropLoot()
    {
        if(lootSystem != null)
        {
            GameObject toDrop = lootSystem.GetLoot();
            if(toDrop != null)
            {
                Instantiate(toDrop, transform.position, Quaternion.identity);
            }
        }
    }

    public void StopBeingHit()
    {
        anim.SetBool("isBeingHit", false);
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToRight > distanceToLeft) target = rightLimit;
        else target = leftLimit;

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x < target.position.x)
        {
            rotation.y = 180;
        } else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
}
