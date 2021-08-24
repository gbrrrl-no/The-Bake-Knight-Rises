using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    #region Public Variables
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.96f;
    public LayerMask enemyLayers;
    public float cooldownTimer; // Cooldown between attacks
    #endregion

    #region Private Variables
    private bool isOnCooldown;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = cooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnCooldown)
        {
            Cooldown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ConsumeWeapon();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {
        if (!isOnCooldown)
        {
            cooldownTimer = intTimer; //Reset timer when Player enter Attack Ranger

            animator.SetTrigger("Attack");
            Vector2 position = attackPoint.position;
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, attackRange, enemyLayers);

            foreach (var collidedEnemy in hits)
            {
                Debug.Log("Enemy hit");
                GameObject root = collidedEnemy.transform.root.gameObject;
                root.GetComponent<Enemy_Behaviour>().TakeDamage(20);

                //Debug.Log(collidedEnemy.GetComponent<Enemy_Behaviour>().curHealth);            
            }

            isOnCooldown = true;
        }
    }

    void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            isOnCooldown = false;
            cooldownTimer = intTimer;
        }
    }

    void ConsumeWeapon()
    {
        gameObject.GetComponent<Player_Stats>().ConsumeWeaponCharge();
    }
}
