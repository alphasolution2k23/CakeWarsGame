using UnityEngine;
using EmeraldAI;

public class CannonBall : MonoBehaviour 
{
    [SerializeField]
    GameObject deathEffect;
    public bool shootByEnemy = false;

    //void OnCollisionEnter(Collision collision)
    //{
    //    Instantiate(deathEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
    //    if (collision.gameObject.CompareTag("ground"))
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //    if (collision.gameObject.CompareTag("enemy"))
    //    {
    //        collision.gameObject.GetComponent<EmeraldAISystem>().Damage(10);
    //        Destroy(gameObject);
    //    }
        

    //}

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("enemy"))
        {
           
                Instantiate(deathEffect, transform.position, Quaternion.identity);

                switch (GameManager.instance.currentWeapon)
                {
                    case CurrentWeapon.CupCake:
                        GameManager.instance.shootingController.lastShotTime = 0f;
                        foreach (var item in GameManager.instance.weaponHandler.Weapon)
                        {
                            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                            {
                                other.gameObject.GetComponent<EmeraldAISystem>().Damage(item.damage);

                            }
                        }
                        break;
                    case CurrentWeapon.SmartPhone:
                        GameManager.instance.shootingController.lastShotTime = 0f;
                        foreach (var item in GameManager.instance.weaponHandler.Weapon)
                        {
                            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                            {
                                other.gameObject.GetComponent<EmeraldAISystem>().Damage(item.damage);

                            }
                        }
                        break;
                    case CurrentWeapon.Roses:
                        GameManager.instance.shootingController.lastShotTime = 0f;
                        foreach (var item in GameManager.instance.weaponHandler.Weapon)
                        {
                            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                            {
                                other.gameObject.GetComponent<EmeraldAISystem>().Damage(item.damage);

                            }
                        }
                        break;
                    case CurrentWeapon.PoisonousChocolate:
                        GameManager.instance.shootingController.lastShotTime = 0f;
                        foreach (var item in GameManager.instance.weaponHandler.Weapon)
                        {
                            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                            {
                                other.gameObject.GetComponent<EmeraldAISystem>().Damage(item.damage);

                            }
                        }
                        break;
                    case CurrentWeapon.ExplosiveCake:
                        GameManager.instance.shootingController.lastShotTime = 0f;
                        foreach (var item in GameManager.instance.weaponHandler.Weapon)
                        {
                            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                            {
                                other.gameObject.GetComponent<EmeraldAISystem>().Damage(item.damage);

                            }
                        }
                        break;

                
            }
            Destroy(gameObject);
        }
    }



}
