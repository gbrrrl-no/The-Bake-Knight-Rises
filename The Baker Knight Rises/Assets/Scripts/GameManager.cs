using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public GameObject CompleteLevelUI;

    public GameObject enemyPig;
    public const int numMaxOfEnemies = 5;
    private int numOfEnemies = 0;
    private float lastEnemyCreated = 0;

    private void Start()
    {
        CreateEnemyPig();
    }

    private void Update()
    {
        // criar inimigo a cada 5 segundos
        if (Time.timeSinceLevelLoad > lastEnemyCreated + 5 && numOfEnemies <= numMaxOfEnemies)
        {
            CreateEnemyPig();
        }
    }

    private void CreateEnemyPig()
    {
        GameObject enemyPigGO = Instantiate(enemyPig);
        enemyPigGO.SetActive(true);
        lastEnemyCreated = Time.timeSinceLevelLoad;
        numOfEnemies++;
    }

    public void CompleteLevel() {
        CompleteLevelUI.SetActive(true);
    }

    public void EndGame() {
        if(gameHasEnded == false) {
            gameHasEnded = true;
            Debug.Log("GAME OVER.");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}