using UnityEngine;
using TurretTypes;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Tower Defence/EnemyScriptableObject", order = 0)]
public class EnemyScriptableObject : ScriptableObject 
{
    [Range(10, 50)]
    public int healthPoints  = 10;

    [Range(1, 10)]
    public int movementSpeed = 1;

    public TurretType ignoredTurret = TurretType.None;
}
