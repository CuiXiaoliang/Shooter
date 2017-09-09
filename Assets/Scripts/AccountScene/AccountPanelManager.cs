using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class AccountPanelManager : MonoBehaviour
{
    #region Para

    public Transform RankingListMenu;
    public Transform TopPopMenu;
    public Text TipsText;
    public Text NameText;

    public string SaveFilePath;
    public bool IsClearData = false;

    private List<PlayerScore> _playerScores;
    private GameObject _dataTransmitHelper;
    private PlayerScore _newPlayerScore;
    #endregion

    #region UnityInternalCall

    // Use this for initialization
    void Start()
    {
        _dataTransmitHelper = GameObject.Find("DataTransmitHelper");
        _playerScores = new List<PlayerScore>();
        TopPopMenu.localScale = Vector3.one * 0.5f;
        TopPopMenu.DOScale(1, 1f)
            .SetEase(Ease.OutBack);
        GetDataFromLocal(SaveFilePath);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion



    #region OnClickBtnCall

    public void OnClickDoneBtn()
    {
        if (NameText.text != "")
        {
            TopPopMenu.DOScale(0, 1f)
                .SetEase(Ease.InBack);
            string name = NameText.text;
            
            GetPlayerScore(name);
            SavePlayerScoreDate(SaveFilePath);
        }
        else
        {
            TipsText.text = "Name can't be empty!";
        }
    }

    #endregion

    #region InternalCall

    private void GetPlayerScore(string name)
    {
        string date = DateTime.Now.ToString();
        int score;
        int killNum;
        if (_dataTransmitHelper == null)
        {
            score = 0;
            killNum = 0;
        }
        else
        {
            score = _dataTransmitHelper.GetComponent<DataTransmitHelper>().score;
            killNum = _dataTransmitHelper.GetComponent<DataTransmitHelper>().num;
        }

        _newPlayerScore = new PlayerScore(name, date, score, killNum);
        _playerScores.Add(_newPlayerScore);
    }

    private bool GetDataFromLocal(string saveFilePath)
    {
        FileStream file = new FileStream(saveFilePath, FileMode.OpenOrCreate);
        file.Close();
        if (IsClearData)
        {
            file = new FileStream(saveFilePath, FileMode.Create);
            file.Close();
        }
        
        string jsonText = File.ReadAllText(saveFilePath);
        if (jsonText == "")
            return false;

        JsonData jsonData = JsonMapper.ToObject(jsonText);
        foreach (JsonData eachData in jsonData)
        {
            string name = eachData["Name"].ToString();
            string date = eachData["SaveDate"].ToString();
            int score = int.Parse(eachData["Score"].ToString());
            int killNum = int.Parse(eachData["KillNum"].ToString());
            PlayerScore playerScore = new PlayerScore(name, date, score, killNum);
            _playerScores.Add(playerScore);
            _playerScores.Sort((a, b) => a.Score.CompareTo(b));
        }
        return true;
    }

    private bool SavePlayerScoreDate(string filePath)
    {
        string text = JsonMapper.ToJson(_playerScores);
        File.WriteAllText(filePath, text);
        return true;
    }

    #endregion
}
