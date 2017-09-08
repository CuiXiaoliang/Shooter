using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private static List<GameObject> _enemies;
    private bool _isPaused;
    public bool IsGamePaused { get { return _isPaused; } }

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();
        _isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Interface

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    public void Reset()
    {
        
    }

    public void Pause()
    {
        _isPaused = true;
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    public void Continue()
    {
        _isPaused = false;
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
#endregion
}
