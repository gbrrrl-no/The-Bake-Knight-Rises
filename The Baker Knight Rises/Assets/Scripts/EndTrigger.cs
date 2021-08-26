using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    
    public GameManager gameManager;
    public GameObject infoText;
    public GameObject player;

    void OnTriggerEnter2D (Collider2D collider) {
        GameObject root = collider.transform.root.gameObject;
        if(root.CompareTag("Player")) {
            if (gameManager.numMaxOfEnemies <= player.GetComponent<Player_Stats>().enemiesKilled)
            {
                gameManager.CompleteLevel();
            }
            else
            {
                infoText.GetComponent<Text>().text = "Você precisa matar todos os inimigos primeiro!";
                infoText.SetActive(true);
                Invoke(nameof(DisableInfoText), 3);
            }
        }
    }

    private void DisableInfoText()
    {
        infoText.SetActive(false);
    }
}