using System.Linq;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    public ScoreRowUi rowUi;
    public ScoreManager scoreManager;
    
    void OnEnable()
    {
        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<ScoreRowUi>();
            row.playerName.text = scores[i].playerName;
            row.waveNum.text = scores[i].waveNum.ToString();
            row.score.text = scores[i].score.ToString();
        }
        ScoreManager.SaveScore();
    }

}
