using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Para

    private Text _text;
    private float _restSeconds;
    private bool _isPaused;

    public int RestSeconds{get { return Convert.ToInt32(_restSeconds); }}

    public int GameTimeInMin = 3;

    
    #endregion

    #region UnityInternalCall

    void Awake()
    {
        _text = GetComponent<Text>();
        _restSeconds = GameTimeInMin * 60;
        _isPaused = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused)
        {
            _restSeconds -= Time.deltaTime;
            if (_restSeconds <= 0)
            {
                _text.text = "Rest seconds: 0s";
                GameOver();
                return;
            }
            _text.text = "Rest seconds: " + Convert.ToInt32(_restSeconds) + "s";
        }
    }

    #endregion

    #region Interface

    public void PauseGame()
    {
        _isPaused = true;
    }

    public void ContinueGame()
    {
        _isPaused = false;
    }
    

    #endregion

    #region InternalCall


    private void GameOver()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainSceneGameManager>().GameOver();
    }

    #endregion

}
