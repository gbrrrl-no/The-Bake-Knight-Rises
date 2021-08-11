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
    public BoxCollider2D playerBoxCollider;
    public CircleCollider2D playerCircleCollider;
    #endregion

    #region Private Variables
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool isPlayerInRange;
    private bool isOnCooldown;
    private float intTimer;
    private bool facingLeft;
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
                hitLeft = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
                hitRight = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, rayCastMask);
                RaycastDebugger();
            }

            if (isOnCooldown)
            {
                Cooldown();
                anim.SetBool("Attack", false);
            }

            //When Player is detected
            if (hitLeft.collider != null || hitRight.collider != null)
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

    void LateUpdate (){
 
        Vector3 localScale = transform.localScale;
        if (hitLeft.collider != null)
        {
            facingLeft = true;
        }
        else if (hitRight.collider != null)
        {
            facingLeft = false;
        }
        if (((facingLeft ) && (localScale.x < 0 )) || ((!facingLeft) && (localScale.x > 0 ))) 
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }


    private void OnTriggerStay2D(Collider2D trigger)
    {
        //7 == Player Layer, Get Dinamycally later
        if(trigger.gameObject.layer == 7)
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
        anim.SetBool("isBeingHit", true);
        curHealth -= dmg;
        curHealth = Mathf.Max(curHealth, 0);
        healthBar.SetHealth(curHealth);
        if (curHealth == 0){
            DiePig();
        }
    }

    public void DiePig()
    {
        Physics2D.IgnoreCollision(playerCircleCollider, gameObject.transform.Find("Enemy_Pig_Collider/Box_Collider").GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(playerBoxCollider, gameObject.transform.Find("Enemy_Pig_Collider/Box_Collider").GetComponent<BoxCollider2D>());
        anim.SetBool("isDead", true);
        healthBar.SetVisible(false);
    }

    public void StopBeingHit()
    {
        anim.SetBool("isBeingHit", false);
    }
}
