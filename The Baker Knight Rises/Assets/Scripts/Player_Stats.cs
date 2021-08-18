using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int curHealth;
    public int maxHealth = 100;
    public Animator animator;
    public HealthBar healthBar;

    private int collectedMeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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

    public void increaseMeatCollection()
    {
        collectedMeat++;
    }
}
