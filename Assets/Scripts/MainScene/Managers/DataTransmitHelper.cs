using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransmitHelper : MonoBehaviour
{
    public int score;

    public int num;
	// Use this for initialization
	void Start ()
    {
		GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    #region Interface

    public void Reset()
    {
        score = 0;
        num = 0;
    }

    #endregion
}
