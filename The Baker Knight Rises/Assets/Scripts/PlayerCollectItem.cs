using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectItem : MonoBehaviour
{
    
    Player_Stats statsScript;
    public Transform player;
    public LayerMask lootLayer;
    public Vector2 size;

    private void Awake()
    {
        GameObject root = transform.root.gameObject;
        statsScript = root.GetComponent<Player_Stats>();
    }

    void Update()
    {
            Vector2 position = player.position;
            Collider2D hit = Physics2D.OverlapBox(position, size, 0, lootLayer);

            if(hit != null){
                if(hit.gameObject.tag == "Loot"){
                    statsScript.increaseMeatCollection();
                    Destroy(hit.transform.root.gameObject);
                }
            }
    }
}
