using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //Min dist for attack
    public float moveSpeed;
    public float cooldownTimer; // Cooldown between attacks
    public int curHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool isPlayerInRange;
    private bool isOnCooldown;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = cooldownTimer;
        anim = GetComponent<Animator>();
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isDead") == false)
        {
            if (isPlayerInRange)
            {
                hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
                RaycastDebugger();
            }

            if (isOnCooldown)
            {
                Cooldown();
                anim.SetBool("Attack", false);
            }

            //When Player is detected
            if (hit.collider != null)
            {
                EnemyLogic();
            } else
            {
                isPlayerInRange = false;
            }

            if(!isPlayerInRange)
            {
                anim.SetBool("canWalk", false);
                StopAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Player")
        {
            target = trigger.gameObject;
            isPlayerInRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        } else if(!isOnCooldown)
        {
            Attack();
        }

        
    }
    
    void Move()
    {
        anim.SetBool("canWalk", true);
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).ToString());
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Pig_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        cooldownTimer = intTimer; //Reset timer when Player enter Attack Ranger

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
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

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        } else if(distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        isOnCooldown = true;
    }

    public void TakeDamage(int dmg)
    {
        if(curHealth - dmg <= 0){
            curHealth -= dmg;
            curHealth = Mathf.Max(curHealth, 0);
            healthBar.SetHealth(curHealth);
            anim.SetBool("isDead", true);
            healthBar.SetVisible(false);
        }else
        {
            anim.SetBool("isBeingHit", true);
            curHealth -= dmg;
            curHealth = Mathf.Max(curHealth, 0);
            healthBar.SetHealth(curHealth);
        }
        
    }

    public void stopBeingHit()
    {
        anim.SetBool("isBeingHit", false);
    }
}
