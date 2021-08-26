using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    public Animator animator;

    public int curHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    private int weaponCharges = 3;
    public GameObject[] ChargesCounter;

    private int collectedMeat = 0; 
    public Text counterText;

    const int restoreHealth = 50;

    public int enemiesKilled = 0;

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
        counterText.text = collectedMeat.ToString();
    }

    public void ConsumeWeaponCharge()
    {
        if (weaponCharges > 0)
        {
            weaponCharges--;
            ChargesCounter[weaponCharges].SetActive(false);
            Debug.Log("Consumed charge and healed +"+(Mathf.Min(100 - curHealth, restoreHealth)) +" health");
            curHealth += restoreHealth;
            curHealth = Mathf.Min(curHealth, 100);
            healthBar.SetHealth(curHealth);
        }
        else 
        {
            Debug.Log("No charges left");
        }
    }
}
