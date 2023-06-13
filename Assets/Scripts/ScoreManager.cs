using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static List<ScoreLine> scores;
    
    public static ScoreManager instance;

    private void Awake() { 
        if(instance != null){
            Debug.Log("More than one ScoreManager!");
            return;
        }
        instance = this;
    }

    void Start()
    {     
        using(StreamReader sr = new StreamReader(Application.dataPath + "//UserScores.json"))
            scores = JsonHelper.FromJson<ScoreLine>(sr.ReadToEnd()).ToList();
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
        using(StreamWriter sw = new StreamWriter(Application.dataPath + "//UserScores.json"))
            sw.Write(JsonHelper.ToJson<ScoreLine>(scores.ToArray()));
           
    }

}
