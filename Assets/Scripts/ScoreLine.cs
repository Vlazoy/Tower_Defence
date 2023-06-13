[System.Serializable]
public class ScoreLine
{
    public string playerName;
    public int waveNum;
    public int score;

    public ScoreLine(string name, int wave, int _score){
        playerName  = name;
        waveNum = wave;
        score = _score;
    }
}
