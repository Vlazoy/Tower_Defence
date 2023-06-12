using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private TurretBlueprint squareTurret,triangleTurret,spikeTurret;
    BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }

    public void SelectSquareTurret(){
        buildManager.TurretToBuild = squareTurret;
    }

    public void SelectTriangleTurret(){        
        buildManager.TurretToBuild = triangleTurret;
    }

    public void SelectSpikeTurret(){      
        buildManager.TurretToBuild = spikeTurret;
    }

}
