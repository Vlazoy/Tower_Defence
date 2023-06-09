using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{
    private int baseEnemyCount;
    private int tankEnemyCount;
    private int speedEnemyCount;

    public int[] GetWave{
        get{
            return new[] {baseEnemyCount, tankEnemyCount, speedEnemyCount, baseEnemyCount + tankEnemyCount + speedEnemyCount};
        }
    }

    public override string ToString()
    {
        int[] temp = GetWave;
        return "" + temp[0] + " " + temp[1] + " " + temp[2] + " " + temp[3] + " ";
    }
}

[System.Serializable]
public class Waves{
    public Wave[] waves;

    public override string ToString()
    {
        string message = "";
        foreach (Wave item in waves)
        {
            message += item.ToString() + '\n';
        }
        return message;
    }
}

public class WaveSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject baseEnemy, tankEnemy, speedEnemy;
    private bool spawning;

    private int waveNum = 1;
    private Wave[] waves;
    private float countDown = 2f;

    void Start()
    {    
    }
    
    void Update()
    {
        if(countDown <= 0){
            StartCoroutine(SpawnWave());
            countDown = 5f;
        }
        countDown -= Time.deltaTime;
    }
 
    private IEnumerator SpawnWave(){
        for (int i = 0; i < waveNum; i++)
        {
            Debug.Log("Base enemy spawned!");
            SpawnEnemy(baseEnemy);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < waveNum; i++)
        {
            Debug.Log("Tank enemy spawned!");
            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < waveNum; i++)
        {
            Debug.Log("Speed enemy spawned!");
            yield return new WaitForSeconds(0.2f);
        }
        waveNum++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
    

}
