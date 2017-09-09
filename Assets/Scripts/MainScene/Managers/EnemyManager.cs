using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyController))]
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public Transform[] patrolPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        //暂停模式下直接返回
        EnemyController enemyController = GetComponent<EnemyController>();
        if(enemyController.IsGamePaused)
            return;
        //主角死亡直接返回
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }
        // 设置出生点并初始化敌人
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        GameObject newEnemy = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        // 低概率设置巡逻敌人
        if (Random.Range(0, 10) > 7)
        {
            newEnemy.GetComponent<EnemyAttack>().SetPatrol();
            newEnemy.GetComponent<EnemyMovement>().SetPatrol(patrolPoints.ToList());
        }
        enemyController.AddEnemy(newEnemy);
    }
}
