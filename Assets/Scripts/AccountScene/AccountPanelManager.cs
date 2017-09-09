using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountPanelManager : MonoBehaviour
{
    #region Para

    public Transform RankingListMenu;
    public Transform TopPopMenu;
    public Text TipsText;
    public Text NameText;
    public GameObject RankItemPrefab;
    public GameObject GridLayout;

    public string SaveFilePath;
    public bool IsClearData = false;

    private List<PlayerScore> _playerScores;
    private GameObject _dataTransmitHelper;
    private PlayerScore _newPlayerScore;
    private bool _isBtnDown = false;
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

    #region InternalCall
    /// <summary>
    /// 从本地加载排名数据
    /// </summary>
    /// <param name="saveFilePath"></param>
    /// <returns></returns>
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
        }
        return true;
    }

    /// <summary>
    /// 获取此局游戏的成绩类
    /// </summary>
    /// <param name="name"></param>
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
        _playerScores.Sort(ScoreCompare);
        SavePlayerScoreDate(SaveFilePath);
    }

    private int ScoreCompare(PlayerScore scoreA, PlayerScore scoreB)
    {
        if (scoreA.Score > scoreB.Score)
            return -1;
        else if (scoreA.Score == scoreB.Score)
            return 0;
        else
            return 1;
    }

    
    /// <summary>
    /// 保存更新后的排名到本地
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private bool SavePlayerScoreDate(string filePath)
    {
        string text = JsonMapper.ToJson(_playerScores);
        File.WriteAllText(filePath, text);
        return true;
    }

    /// <summary>
    /// 排名窗口弹出动画
    /// </summary>
    private void DoRankListAppear()
    {
        RankingListMenu.DOScale(1, 1f).SetEase(Ease.OutBack);
        RankingListMenu.GetComponent<Image>().DOFade(1, 1).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// 可视化排名信息
    /// </summary>
    private void PutItemsIntoRank()
    {
        int count = 0;
        GameObject rankItem = Instantiate(RankItemPrefab, GridLayout.transform);
        PutTextIntoItem(rankItem, _newPlayerScore);
        ChangeRankItemTextColor(rankItem, new Color(0,1,1,1));
        foreach (var playerScore in _playerScores)
        {
            if (count != _playerScores.IndexOf(_newPlayerScore))
            {
                rankItem = Instantiate(RankItemPrefab, GridLayout.transform);
                PutTextIntoItem(rankItem, playerScore);
            }
            count++;
        }
        while (count < 5)
        {
            rankItem = Instantiate(RankItemPrefab, GridLayout.transform);
            count++;
        }
    }

    /// <summary>
    /// 实装文字信息
    /// </summary>
    /// <param name="rankItem"></param>
    /// <param name="playerScore"></param>

    private void PutTextIntoItem(GameObject rankItem, PlayerScore playerScore)
    {
        rankItem.GetComponent<RankItemControl>().RankText.text = (_playerScores.IndexOf(playerScore) + 1).ToString();
        rankItem.GetComponent<RankItemControl>().NameText.text = playerScore.Name;
        rankItem.GetComponent<RankItemControl>().ScoreText.text = playerScore.Score.ToString();
        rankItem.GetComponent<RankItemControl>().KillText.text = playerScore.KillNum.ToString();
        rankItem.GetComponent<RankItemControl>().DateText.text = playerScore.SaveDate;
    }

    private void ChangeRankItemTextColor(GameObject rankItem, Color color)
    {
        rankItem.GetComponent<RankItemControl>().RankText.color = color;
        rankItem.GetComponent<RankItemControl>().NameText.color = color;
        rankItem.GetComponent<RankItemControl>().ScoreText.color = color;
        rankItem.GetComponent<RankItemControl>().KillText.color = color;
        rankItem.GetComponent<RankItemControl>().DateText.color = color;
    }


    #endregion

    #region OnClickBtnCall

    public void OnClickDoneBtn()
    {
        if (!_isBtnDown)
        {
            if (NameText.text != "")
            {
                TopPopMenu.DOScale(0, 1f)
                    .SetEase(Ease.InBack)
                    .OnComplete(DoRankListAppear);
                string name = NameText.text;

                GetPlayerScore(name);
                PutItemsIntoRank();
            }
            else
                TipsText.text = "Name can't be empty!";
            _isBtnDown = true;
        }
        
    }

    public void OnClickMenuBtn()
    {
        SceneManager.LoadScene(0);
        Destroy(_dataTransmitHelper);
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    #endregion
}
