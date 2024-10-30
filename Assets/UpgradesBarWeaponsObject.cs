using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesBarWeaponsObject : MonoBehaviour
{
    [SerializeField] WeaponUpgradeStatUI stat1;
    [SerializeField] WeaponUpgradeStatUI stat2;
    [SerializeField] WeaponUpgradeStatUI stat3;
    string weaponName;

    public void RefreshUI(string _weaponName)
    {
        weaponName = _weaponName;

        int stat1Lvl = PlayerPrefs.GetInt(_weaponName + "Stat1", 0);
        int stat2Lvl = PlayerPrefs.GetInt(_weaponName + "Stat2", 0);
        int stat3Lvl = PlayerPrefs.GetInt(_weaponName + "Stat3", 0);

        stat1.RefreshUI(stat1Lvl);
        stat2.RefreshUI(stat2Lvl);
        stat3.RefreshUI(stat3Lvl);
    }

    public void UpdateStat1()
    {
        PlayerPrefs.SetInt(weaponName + "Stat1", PlayerPrefs.GetInt(weaponName + "Stat1", 0) + 1);

        if (PlayerPrefs.GetInt(weaponName + "Stat1") > 5)
        {
            PlayerPrefs.SetInt(weaponName + "Stat1", 5);
        }
    }
    public void UpdateStat2()
    {
        PlayerPrefs.SetInt(weaponName + "Stat2", PlayerPrefs.GetInt(weaponName + "Stat2", 0) + 1);

        if (PlayerPrefs.GetInt(weaponName + "Stat2") > 5)
        {
            PlayerPrefs.SetInt(weaponName + "Stat2", 5);
        }
    }
    public void UpdateStat3()
    {
        PlayerPrefs.SetInt(weaponName + "Stat3", PlayerPrefs.GetInt(weaponName + "Stat3", 0) + 1);

        if (PlayerPrefs.GetInt(weaponName + "Stat3") > 5)
        {
            PlayerPrefs.SetInt(weaponName + "Stat3", 5);
        }
    }
}
