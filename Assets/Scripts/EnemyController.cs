using UnityEngine;
using PathCreation;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyData;
    private float distance = 0;
    private PathCreator path;
    void Start()
    {
        path = GameObject.FindGameObjectWithTag("EnemyPath").GetComponent<PathCreator>();
    }

    void Update()
    {   
        distance += enemyData.movementSpeed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(distance);
        transform.rotation = path.path.GetRotationAtDistance(distance);
    }
}
