using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    [SerializeField]
    private TurretScriptableObject turretData;
    private Transform target;
    private float fireCountdown = 0f;
    private Transform firePoint;
    public GameObject bulletPrefab;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= turretData.shootRange)
		{
			target = nearestEnemy.transform;
			//targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			return;
		}
        
		LockOnTarget();

        if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / turretData.shootSpeed;
			}

			fireCountdown -= Time.deltaTime;
	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turretData.rotateSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
	}
    
	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		BulletController bullet = bulletGO.GetComponent<BulletController>();

		if (bullet != null)
			bullet.Seek(target);
	}
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, turretData.shootRange);
    }
}
