using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyController))]
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        EnemyController enemyController = GetComponent<EnemyController>();
        if(enemyController.IsGamePaused)
            return;
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        GameObject newEnemy = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        enemyController.AddEnemy(newEnemy);
    }
}
