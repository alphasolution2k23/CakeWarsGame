using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shoot;

public class SodaShoot : MonoBehaviour
{
    public GameObject FireEffect;
    private GameObject ComponentFound;
    public void ShootNow()
    {
        GameManager.instance.shootingController.anim.SetTrigger("GunShoot");

        FireEffect.SetActive(true);
        foreach (var item in WaveManager.WMmanager.ActiveEnemies)
        {
            Debug.Log("EnemyName" + item.name);
            float distance = Vector3.Distance(transform.position, item.transform.position);
            ComponentFound = item;
            SendDamage(distance);
        }
    }
    public void SendDamage(float DistanceDamage)
    {
        float damage = DistanceDamage / 2;
        GameManager.instance.shootingController.lastShotTime = 0f;
        foreach (var item in GameManager.instance.weaponHandler.Weapon)
        {
            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
            {
                if (ComponentFound.GetComponent<EmeraldAISystem>())
                {
                    int dmg = item.damage + (int)damage;
                    Debug.Log("SendingDamage+=" + dmg);
                    ComponentFound.GetComponent<EmeraldAISystem>().Damage(dmg);

                }

            }
        }
    }
}
