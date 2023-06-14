using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static List<ScoreLine> scores = new List<ScoreLine>();

    void Start()
    {   
        if(File.Exists(Application.dataPath + "/UserScores.json"))
            scores = JsonHelper.FromJson<ScoreLine>(File.ReadAllText(Application.dataPath + "/UserScores.json")).ToList();
    }

    public IEnumerable<ScoreLine> GetHighScores()
    {
        return scores.OrderByDescending(x => x.score);
    }

    public static void AddScore()
    {   
        scores.Add(new ScoreLine(Environment.UserName, WaveSpawner.GetWaveNum, PlayerStats.GetScore));
    }

    public static void SaveScore()
    {
        File.WriteAllText(Application.dataPath + "/UserScores.json", JsonHelper.ToJson<ScoreLine>(scores.ToArray()));
    }

}
