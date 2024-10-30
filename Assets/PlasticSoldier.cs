using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticSoldier : MonoBehaviour
{
    Animator anim;
    public float rotationSpeed;
    public float shootCooldown = 1f;
    private float lastShootTime;
    private Transform target;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    void Start()
    {
        anim = GetComponent<Animator>();
        lastShootTime = Time.time; // Ensure the soldier can not shoot immediately at start

        shootCooldown = Random.Range(3f, 5f);
    }

    private void Update()
    {
        #region Check if Target Available
        if (WaveManager.WMmanager.ActiveEnemies.Count == 0)
            return;

        target = WaveManager.WMmanager.ActiveEnemies[0].transform;
        if (target == null)
            return;
        #endregion
        RotateToTarget();

        #region Check If Cooling Down
        if (Time.time - lastShootTime < shootCooldown)
        {
            return;
        }
        #endregion
        Shoot();
    }

    private void RotateToTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        anim.SetTrigger("Shoot");
        lastShootTime = Time.time;

        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}