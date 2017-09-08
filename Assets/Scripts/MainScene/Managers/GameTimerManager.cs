using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerManager : MonoBehaviour
{
    public int GameTimeInMin = 3;

    private Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    int restTime = GameTimeInMin * 60 - Convert.ToInt32(Time.realtimeSinceStartup);
        if(restTime <= 0)
            GameOver();
	    _text.text = "Rest seconds: " + restTime + "s";

	}

    private void GameOver()
    {
        
    }
}
