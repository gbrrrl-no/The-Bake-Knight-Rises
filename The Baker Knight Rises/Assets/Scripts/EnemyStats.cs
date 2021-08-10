using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int curHealth;
    public int maxHealth = 100;
    public Animator animator;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        animator.SetBool("isBeingHit", true);
        curHealth -= dmg;
        curHealth = Mathf.Max(curHealth, 0);
        healthBar.SetHealth(curHealth);
    }

    public void stopBeingHit()
    {
        animator.SetBool("isBeingHit", false);
    }


}
