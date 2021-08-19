using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;
    private bool isPlayerInRange;
    private Animator anim;
    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(isPlayerInRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Pig_Attack")) {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject root = collider.transform.root.gameObject;
        if (root.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GameObject root = collider.transform.root.gameObject;
        if (root.CompareTag("Player"))
        {
            isPlayerInRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.isPlayerInRange = false;
            enemyParent.SelectTarget();
        }
    }
}
