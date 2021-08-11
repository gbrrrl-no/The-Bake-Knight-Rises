using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trigger)
    {
        GameObject root = trigger.transform.root.gameObject;
        if (root.tag == "Player")
        {
            root.GetComponent<Player_Stats>().TakeDamage(5);
        }
    }

}
