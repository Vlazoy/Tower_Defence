using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    private void Awake() { 
        if(instance != null){
            return;
        }
        instance = this;
    }

    public bool HasMoney{get => PlayerStats.ChangeMoney < instance.turretToBuild.cost;}
    public bool CanBuild{get => instance.TurretToBuild == null;}

    private TurretBlueprint turretToBuild;

    public TurretBlueprint TurretToBuild {
        get => turretToBuild;
        set => turretToBuild = value;
    }

    public void BuildTurretOn(NodeController node){
   
        GameObject _turretToBuild = instance.TurretToBuild.prefab;

        node.turret = (GameObject)Instantiate(_turretToBuild, new Vector3(node.transform.position.x, node.transform.position.y, -1f), Quaternion.identity);
        
        PlayerStats.ChangeMoney = -instance.TurretToBuild.cost;

        instance.TurretToBuild = null;
    }
}
