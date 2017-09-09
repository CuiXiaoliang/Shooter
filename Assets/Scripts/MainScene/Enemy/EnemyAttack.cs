using System;
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    #region Para

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private enum State
    {
        IsAttacking,
        IsPatroling
    }

    private State _enemyState;

    private Animator anim;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;

    #endregion

    #region UnityInternalCall

    void Awake()
    {
        timer = 0;
        _enemyState = State.IsAttacking;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && _enemyState == State.IsAttacking)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        if (_enemyState == State.IsAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }
        }
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    #endregion

    #region InternalCall
    /// <summary>
    /// 攻击函数
    /// </summary>
    private void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    #endregion

    #region Interface
    /// <summary>
    /// 设置敌人攻击行为为巡逻模式
    /// </summary>
    public void SetPatrol()
    {
        _enemyState = State.IsPatroling;
        playerInRange = false;
    }
    /// <summary>
    /// 设置敌人攻击行为为攻击模式
    /// </summary>

    public void SetAttrack()
    {
        _enemyState = State.IsAttacking;
    }

    #endregion
}
