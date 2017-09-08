using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Interface

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    #endregion
}
