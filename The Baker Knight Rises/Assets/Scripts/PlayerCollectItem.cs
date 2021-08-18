using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectItem : MonoBehaviour
{
    
    Player_Stats statsScript;

    private void Awake()
    {
        GameObject root = transform.root.gameObject;
        statsScript = root.GetComponent<Player_Stats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject root = collision.transform.root.gameObject;
        if(root.CompareTag("Loot"))
        {
            statsScript.increaseMeatCollection();
        }
    }
}
