using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore
{
    public string Name { get; set; }
    public string SaveDate { get; set; }

    public int Score { get; set; }
    public int KillNum { get; set; }

    public PlayerScore()
    {
        
    }
    public PlayerScore(string name, string saveDate, int score, int killNum)
    {
        Name = name;
        SaveDate = saveDate;
        Score = score;
        KillNum = killNum;
    }
}
