using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
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
        animator.SetBool("IsBeingHit", true);
        curHealth -= dmg;
        curHealth = Mathf.Max(curHealth, 0);
        healthBar.SetHealth(curHealth);
        if (curHealth == 0)
        {
            animator.SetBool("IsDead", true);
        }
    }

    public void stopBeingHit()
    {
        animator.SetBool("IsBeingHit", false);
    }

}
