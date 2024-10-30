using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class Shoot : MonoBehaviour
{
    public GameObject FireEffect;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public enum ObjectToFire
    {
        PartyBlowers,
        MagicKit,
        ConfettiCannon,
        BubbleCannon
    }
    public ObjectToFire objectToFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private GameObject ComponentFound;
    // Update is called once per frame
    public void ShootNow()
    {
        if (objectToFire == ObjectToFire.PartyBlowers)
        {
          
            // PlayerPrefs.SetInt("DaneceAbilityActive", 1);
            GameManager.instance.shootingController.anim.SetTrigger("GunShoot");

            FireEffect.SetActive(true);
            foreach (var item in WaveManager.WMmanager.ActiveEnemies)
            {
                Debug.Log("EnemyName" + item.name);
                float distance = Vector3.Distance(transform.position, item.transform.position);
                Debug.Log("Enemydistance" + distance);
                if (distance <= 60)
                {
                    Debug.Log("EnemyInRangeName" + item.name);
                    item.GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.NotActive;
                    item.GetComponent<EmeraldAISystem>().IsMoving = false;
                    StartCoroutine(MoveBackWard(item));
                }
            }
           // Invoke("DisableIt", 30f);
        }
        if (objectToFire == ObjectToFire.ConfettiCannon)
        {

            // PlayerPrefs.SetInt("DaneceAbilityActive", 1);
            GameManager.instance.shootingController.anim.SetTrigger("GunShoot");

            FireEffect.SetActive(true);
            foreach (var item in WaveManager.WMmanager.ActiveEnemies)
            {
                Debug.Log("EnemyName" + item.name);
                float distance = Vector3.Distance(transform.position, item.transform.position);
                ComponentFound = item;
                SendDamage(distance);
            }
            // Invoke("DisableIt", 30f);
        }
        if (objectToFire == ObjectToFire.MagicKit)
        {

            // Get the mouse position in world space

            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition.z = Camera.main.nearClipPlane; 
            //projectilePrefab.GetComponent<ProjectileShooter>().TargetPos = Camera.main.ScreenToWorldPoint(mousePosition);
            //projectilePrefab.GetComponent<ProjectileShooter>().TargetPos.y = firePoint.position.y; 
            GameManager.instance.shootingController.anim.SetTrigger("GunShoot");
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = GameManager.instance.shootingController.hand.up * GameManager.instance.shootingController.currentSpeed;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
            //{
            //    projectile.GetComponent<ProjectileShooter>().TargetPos = hit.point;
            //}
            //projectile.GetComponent<ProjectileShooter>().isMoving = true;


        }
        if (objectToFire == ObjectToFire.BubbleCannon)
        {  
            GameManager.instance.shootingController.anim.SetTrigger("GunShoot");
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = GameManager.instance.shootingController.hand.up * GameManager.instance.shootingController.currentSpeed;
        }
    }

    private IEnumerator MoveBackWard(GameObject obj)
    {

        Debug.Log("MovingBack");
        float moveDuration = 5f; // Duration to move backward
        float elapsedTime = 0.0f;

        while (elapsedTime < moveDuration)
        {
            // Move the object backward
            obj.transform.Translate(Vector3.back * 10f * Time.deltaTime);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Yield execution of this coroutine and return to the main loop until the next frame
            yield return null;
        }
        obj.GetComponent<EmeraldAISystem>().IsMoving = false;
        obj.GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.Active;
    }
    public void SendDamage(float DistanceDamage)
    {
        float damage = DistanceDamage / 2;
        switch (GameManager.instance.currentWeapon)
        {
            case CurrentWeapon.ConfettiCannon:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            int dmg = item.damage + (int)damage;
                            Debug.Log("SendingDamage+="+ dmg);
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(dmg);

                        }

                    }
                }
                break;
          

        }
    }
}
