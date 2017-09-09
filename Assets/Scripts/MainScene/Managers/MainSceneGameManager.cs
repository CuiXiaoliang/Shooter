using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneGameManager : MonoBehaviour
{
    #region Para

    private bool _isGameOver = false;
    private bool _isPaused = false;
    public bool IsGamePaused { get { return _isPaused; } }


    public TimerManager timerManager;
    public ScoreManager scoreManager;
    public DataTransmitHelper dataTransmitHelper;
    public MenuPanelManager menuPanelManager;
    public EnemyController enemyController;
    public GameObject player;


    #endregion

    #region UnityInternalCall

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_isGameOver)
            return;
        if (Input.GetKey(KeyCode.Escape))
        {
            if(menuPanelManager.IsMenuMoving)
                return;
            if (_isPaused)
                Continue();
            else
                Pause();
        }
    }

    #endregion

    #region Interface
    public void Pause()
    {
        timerManager.PauseGame();
        menuPanelManager.Pause();
        enemyController.Pause();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerShooting>().enabled = false;
        _isPaused = true;
    }

    public void Continue()
    {
        menuPanelManager.Continue();
        menuPanelManager.transform.DOScale(1, 0.6f)
            .OnComplete(() =>
            {
                timerManager.ContinueGame();
                enemyController.Continue();
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponentInChildren<PlayerShooting>().enabled = true;
                _isPaused = false;
            });

    }

    public void GameOver()
    {
        _isGameOver = true;
        dataTransmitHelper.score = ScoreManager.score;
        dataTransmitHelper.num = ScoreManager.num;
        menuPanelManager.GameOver();
    }

    #endregion

    #region InternalCallFunc



    #endregion


}
