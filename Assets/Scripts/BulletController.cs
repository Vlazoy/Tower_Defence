using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float speed = 70f;
    private float damage;


    public float SetDamage{set => damage = value;}

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
        transform.rotation.eulerAngles.Set(0f, 0f, transform.rotation.eulerAngles.z + 5f);

        float distThisFrame = speed * Time.deltaTime;

        if(dir.magnitude < distThisFrame){
            HitTarget();
        }

        transform.Translate(dir.normalized * distThisFrame, Space.World);
    }

    void HitTarget(){
        target.gameObject.GetComponent<EnemyController>().TakeDmg(damage);
        Destroy(gameObject);
    }

}
