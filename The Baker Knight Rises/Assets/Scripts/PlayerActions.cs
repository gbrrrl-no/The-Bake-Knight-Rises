using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.96f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
            Vector2 position = attackPoint.position;
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, attackRange, enemyLayers);

            foreach (var collidedEnemy in hits)
            {
                Debug.Log("Acertou inimigo");
                GameObject root = collidedEnemy.transform.root.gameObject;
                root.GetComponent<Enemy_Behaviour>().TakeDamage(20);
                
                //Debug.Log(collidedEnemy.GetComponent<Enemy_Behaviour>().curHealth);            
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
