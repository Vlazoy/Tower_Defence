using UnityEngine;
using TurretTypes;

[CreateAssetMenu(fileName = "TurretScriptableObject", menuName = "Tower Defence/TurretScriptableObject", order = 0)]
public class TurretScriptableObject : ScriptableObject {
    public TurretType type = TurretType.Square;

    [Range(1, 100)]
    public int dmg = 1;

    [Range(1, 12)]
    public int shootRange = 10;
}