using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject root = collider.transform.root.gameObject;
        if(root.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = root.transform;
            enemyParent.isPlayerInRange = true;
            enemyParent.hotzone.SetActive(true);
        }
    }
}
