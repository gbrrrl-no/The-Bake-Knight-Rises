using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    public Transform objectToFollow;
    public Vector3 offset;

    public void SetHealth(int health) 
    {
        healthSlider.value = health;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetVisible(bool visible){
        gameObject.SetActive(visible);
    }
    
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(objectToFollow.position + offset);
    }
}
