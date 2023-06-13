using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    [Header("Unity setup")]

    [SerializeField]
    private GameObject baseEnemy, tankEnemy, speedEnemy;

    [SerializeField]
    private TMP_Text waveNumText, waveCountDownText;

    private static int waveNum = 0;
    public static int GetWaveNum{get => waveNum;}

    private int scoreToAdd = 0;
    private Wave[] waves;
    private float countDown = 15.5f;
    private bool enemiesAlive = false;

    void Start()
    {    
        waveNum = 0;
        waves = JsonHelper.FromJson<Wave>(Resources.Load<TextAsset>("Waves").text);
    }
    
    void Update()
    {
        if(countDown <= 0){
            StartCoroutine(SpawnWave());
            enemiesAlive = true;
            countDown = 15.5f;
        }
        if (!enemiesAlive)       
            countDown -= Time.deltaTime;
        waveCountDownText.text = "Next wave in " + Mathf.Round(countDown).ToString();
        waveNumText.text = "Wave num: " + (waveNum + 1).ToString();
    }
 
    private IEnumerator SpawnWave(){
        int waveLength = waves[waveNum].baseEnemyCount + waves[waveNum].tankEnemyCount +waves[waveNum].speedEnemyCount;
        scoreToAdd = waves[waveNum].baseEnemyCount * 5 + waves[waveNum].tankEnemyCount * 10 + waves[waveNum].speedEnemyCount * 15;

        int baseEnemyCounter = 0, tankEnemyCounter = 0, speedEnemyCounter = 0;

        for (int i = 0; i < waveLength; i++)
        {   
            if(baseEnemyCounter++ < waves[waveNum].baseEnemyCount){
                SpawnEnemy(baseEnemy);
                yield return new WaitForSeconds(0.7f);
            }
            
            if(tankEnemyCounter++ < waves[waveNum].tankEnemyCount){
                SpawnEnemy(tankEnemy);
                yield return new WaitForSeconds(1f);
            }

            if(speedEnemyCounter++ < waves[waveNum].speedEnemyCount){
                SpawnEnemy(speedEnemy);
                yield return new WaitForSeconds(0.2f);
            }

        }
        
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
    
    public void CheckEnemies(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <=0){
            PlayerStats.PlusScore = scoreToAdd;
            enemiesAlive = false;
            waveNum++;
            if(waveNum == waves.Length){
                PlayerStats.instance.SaveScore();
            }
        }
    }
}
