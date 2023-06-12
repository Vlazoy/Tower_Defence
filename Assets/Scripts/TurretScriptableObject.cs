using UnityEngine;
using TurretTypes;

[CreateAssetMenu(fileName = "TurretScriptableObject", menuName = "Tower Defence/TurretScriptableObject", order = 0)]
public class TurretScriptableObject : ScriptableObject {
    public TurretType type = TurretType.Square;

    [Range(1, 100)]
    public int dmg = 1;

    [Range(1, 12)]
    public float shootRange = 10;

    [Range(0.5f, 20)]
    public float shootSpeed = 5;

    [Range(3, 20)]
    public float rotateSpeed = 5;
}