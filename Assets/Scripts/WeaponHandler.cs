using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public static WeaponHandler WeaponHandlerInst;
    public weapon [] Weapon;
    [System.Serializable]
    public class weapon 
    {
        public string WeaponName;
        public int weaponId;
        public int WeaponHealth;
        public int WeaponFireSpeed;
        public int WeaponDamage;

        public string NameWeaponHealth;
        public string NameWeaponFireSpeed;
        public string NameWeaponDamage;
        public float cooldownTime = 1;
        public int damage = 10;

        public bool IsShootable;
        public bool IsBottle;
        public bool IsBeachBall;
        public bool IsBirthdayHat;
        public bool IsRcCar;
        public GameObject Weapon;

    }
    // Start is called before the first frame update
    void Start()
    {
        if (WeaponHandlerInst == null)
        {
            WeaponHandlerInst = this;
        }
        UpdateWeapon();
    }

    // Update is called once per frame
    void UpdateWeapon()
    {
        foreach (var upgrade in Weapon)
        {
            int WeaponUpgradeHealth = PlayerPrefs.GetInt("WeaponLevel" + upgrade.NameWeaponHealth);
            Debug.Log("Weapon Upgrade Health: " + WeaponUpgradeHealth);
            upgrade.WeaponHealth = WeaponUpgradeHealth;
             if (upgrade.WeaponHealth == 2)
            {
                
            }
            else if (upgrade.WeaponHealth == 3)
            {

            }
            int WeaponUpgradeFireSpeed = PlayerPrefs.GetInt("WeaponLevel" + upgrade.NameWeaponFireSpeed);
            Debug.Log("Weapon Upgrade Fire Speed:  " + WeaponUpgradeFireSpeed);
            upgrade.WeaponFireSpeed = WeaponUpgradeFireSpeed;
             if (upgrade.WeaponFireSpeed == 1)
             {
                upgrade.cooldownTime -= 0.7f;
             }
            else if (upgrade.WeaponFireSpeed == 2)
            {
                upgrade.cooldownTime -= 0.5f;
            }
            int WeaponUpgradeDamage = PlayerPrefs.GetInt("WeaponLevel" + upgrade.NameWeaponDamage);
            Debug.Log("Weapon Upgrade Damage:   " + WeaponUpgradeDamage);
            upgrade.WeaponDamage = WeaponUpgradeDamage;

            if (upgrade.WeaponDamage == 1)
            {
                upgrade.damage += 10;
            }
            else if (upgrade.WeaponDamage == 2)
            {
                upgrade.damage += 30; 
            }

        }

    }
}
