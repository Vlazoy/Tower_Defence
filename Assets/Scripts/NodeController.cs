using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour
{
    [SerializeField]
    private GameObject outline;
    private GameObject outlineChild;
    public GameObject turret;

    private BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }

    void OnMouseEnter()
    {
        if(buildManager.TurretToBuild == null)
            return;
        outlineChild = Instantiate(outline, transform.position, transform.rotation);
        if(buildManager.HasMoney)
            outlineChild.GetComponent<SpriteRenderer>().color = Color.red;
    }
    
    private void OnMouseExit() {
        if(outlineChild != null)
            Destroy(outlineChild); 
    }
    
    void OnMouseDown()
    {
        if(buildManager.CanBuild || buildManager.HasMoney)
            return;
        
        if(turret != null){
            Debug.Log("You can`t build here!");
            return;
        }
        
        buildManager.BuildTurretOn(this);
    }

}

