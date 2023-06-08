using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour
{
    [SerializeField]
    private GameObject outline;
    private GameObject outlineChild;
    void OnMouseEnter()
    {
        outlineChild = Instantiate(outline, transform.position, transform.rotation);
    }
    
    private void OnMouseExit() {
        Destroy(outlineChild);
    }
    
}

