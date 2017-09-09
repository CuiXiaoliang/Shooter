using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    #region Para

    private List<Vector3> _patrolDestination;

    private Vector3 _nowPatrolDestination;
    private Transform _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private UnityEngine.AI.NavMeshAgent _nav;

    private enum EnemyState
    {
        IsAttacking,
        IsPatroling
    }

    private EnemyState _enemyState;
    #endregion

    #region UnityInternalCall

    void Awake()
    {
        _enemyState = EnemyState.IsAttacking;
        _patrolDestination = new List<Vector3>();
        _nowPatrolDestination = Vector3.zero;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if(!_nav.isActiveAndEnabled)
            return;
        //攻击模式
        if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0 && _enemyState == EnemyState.IsAttacking)
        {
            _nav.SetDestination(_player.position);
        }
        //巡逻模式
        else if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0 && _enemyState == EnemyState.IsPatroling)
        {
            if ((transform.position - _nowPatrolDestination).magnitude <= 2f)
            {
                _nowPatrolDestination = _patrolDestination[Random.Range(0, _patrolDestination.Count)];
            }
            _nav.SetDestination(_nowPatrolDestination);
        }
        else
        {
            _nav.enabled = false;
        }
    }

    #endregion

    #region Interface
    /// <summary>
    /// 设置敌人为攻击模式
    /// </summary>
    public void SetAttrack()
    {
        _enemyState = EnemyState.IsAttacking;
    }

    /// <summary>
    /// 设置敌人为巡逻模式
    /// </summary>
    /// <param name="patrolDestination"></param>
    public void SetPatrol(List<Transform> patrolDestination)
    {
        foreach (var destinaion in patrolDestination)
        {
            _patrolDestination.Add(destinaion.position);
        }
        //避免巡逻点不够
        if (_patrolDestination.Count == 0)
        {
            _patrolDestination.Add(_player.position);
            _patrolDestination.Add(new Vector3(Random.Range(-10f,10f), 0, Random.Range(-10f, 10f)));
        }
        if(_patrolDestination.Count == 1)
            _patrolDestination.Add(_player.position);

        //重置目标巡逻点
        _nowPatrolDestination = _patrolDestination[Random.Range(0, _patrolDestination.Count)];
        _enemyState = EnemyState.IsPatroling;
    }
    /// <summary>
    /// 设置巡逻模式--无参
    /// </summary>
    public void SetPatrol()
    {
        //避免巡逻点不够
        if (_patrolDestination.Count == 0)
        {
            _patrolDestination.Add(_player.position);
            _patrolDestination.Add(new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
        }
        if (_patrolDestination.Count == 1)
            _patrolDestination.Add(_player.position);
        //重置目标巡逻点
        _nowPatrolDestination = _patrolDestination[Random.Range(0, _patrolDestination.Count)];
        _enemyState = EnemyState.IsPatroling;
    }

    #endregion

}
