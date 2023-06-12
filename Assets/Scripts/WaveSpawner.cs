using System.Collections;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Wave
{
    public int baseEnemyCount;
    public int tankEnemyCount;
    public int speedEnemyCount;
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class WaveSpawner : MonoBehaviour
{

    [Header("Unity setup")]

    [SerializeField]
    private GameObject baseEnemy, tankEnemy, speedEnemy;

    [SerializeField]
    private TMP_Text waveNumText, waveCountDownText;

    private int waveNum = 0, scoreToAdd = 0;
    private Wave[] waves;
    private float countDown = 15.5f;
    private bool enemiesAlive = false;

    void Start()
    {    
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
        waveNum++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
    
    public void CheckEnemies(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <=0){
            PlayerStats.PlusScore = scoreToAdd;
            enemiesAlive = false;
        }
    }
}
