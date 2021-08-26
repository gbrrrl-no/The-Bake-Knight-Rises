using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int health = 500;
    public HealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

	public void TakeDamage(int damage)
	{

		health -= damage;
        healthBar.SetHealth(health);

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

}