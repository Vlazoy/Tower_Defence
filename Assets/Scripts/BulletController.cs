using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Transform target;


    [SerializeField]
    private float speed = 70f;

    public void Seek(Transform _target){
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distThisFrane = speed * Time.deltaTime;

        if(dir.magnitude < distThisFrane){
            HitTarget();
        }

        transform.Translate(dir.normalized * distThisFrane, Space.World);
    }

    void HitTarget(){
        Debug.Log("Hit target!");
        Destroy(gameObject);
    }

}
