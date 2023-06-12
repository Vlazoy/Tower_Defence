using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    [SerializeField]
    private TurretScriptableObject turretData;
    private Transform target;
    private float fireCountdown = 0f;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject bulletPrefab;

	[SerializeField]
    private SpriteRenderer turretSprite;


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

		if (nearestEnemy != null && shortestDistance <= turretData.shootRange && nearestEnemy.GetComponent<EnemyController>().IgnoredTurret != turretData.type)
		{
			target = nearestEnemy.transform;
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{	
			fireCountdown = turretData.shootSpeed / 2;
			return;
		}
        
		LockOnTarget();

        if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = turretData.shootSpeed / 1.5f;
		}

		fireCountdown -= Time.deltaTime;
	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;	
		float angle = Vector2.SignedAngle(Vector2.right, dir);
        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turretData.rotateSpeed * 10 * Time.deltaTime);
	}
    
	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		BulletController bullet = bulletGO.GetComponent<BulletController>();

		if (bullet != null){
			bullet.Seek(target);
			bullet.SetDamage = turretData.dmg;
		}
	}
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, turretData.shootRange);
    }
}
