using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D trigger)
    {
        GameObject root = trigger.transform.root.gameObject;
        if (root.tag == "Player")
        {
            root.GetComponent<Player_Stats>().TakeDamage(5);
        }
    }

}
