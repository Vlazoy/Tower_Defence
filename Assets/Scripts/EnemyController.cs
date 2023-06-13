using UnityEngine;
using PathCreation;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyData;
    private float distance = 0;
    private float hp;
    private PathCreator path;

    private WaveSpawner waveSpawner;
    private Transform playerBase;

    public TurretTypes.TurretType IgnoredTurret{get => enemyData.ignoredTurret;}

    void Start()
    {
        hp = enemyData.healthPoints;
        waveSpawner = GameObject.FindGameObjectWithTag("WaveController").GetComponent<WaveSpawner>();
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<Transform>();
        path = GameObject.FindGameObjectWithTag("EnemyPath").GetComponent<PathCreator>();
    }

    void Update()
    {   
        distance += enemyData.movementSpeed * Time.deltaTime / 2;
        transform.position = path.path.GetPointAtDistance(distance);
        transform.rotation = path.path.GetRotationAtDistance(distance);
        DamageBase();
    }

    private void OnDestroy() {
        waveSpawner.CheckEnemies();
    }

    public void TakeDmg(float dmg){
        hp -= dmg;
        if(hp <= 0){
            PlayerStats.ChangeMoney = enemyData.healthPoints;      
            Destroy(gameObject);
        }
    }

    private void DamageBase(){
        if(Vector3.Distance(transform.position, playerBase.position)<1){
            PlayerStats.TakeDmg(1);
            Destroy(gameObject);
        }
    }
}
