using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostersManager : MonoBehaviour
{
    #region Singleton
    public static BoostersManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    public List<BoosterSO> boosters;
    public List<BoosterSO> equippedBoosters;

    private void Start()
    {
        InitBoosters();
    }


    private void InitBoosters()
    {
        equippedBoosters = new List<BoosterSO>();

        foreach (var booster in boosters)
        {
            if (PlayerPrefs.GetInt("BoosterPurch" + booster.boosterName) == 1)
            {
                booster.isPurchased = true;

                if (PlayerPrefs.GetInt("BoosterEquip" + booster.boosterName) == 1)
                {
                    booster.isEquipped = true;
                    equippedBoosters.Add(booster);
                }
                else
                {
                    booster.isEquipped = false;
                }

                if(PlayerPrefs.GetInt("BoosterUpdLvls" + booster.boosterName) > 0)
                {
                    booster.levelsUpgraded = PlayerPrefs.GetInt("BoosterUpdLvls" + booster.boosterName);
                    Debug.Log("Booster Lvl is " + booster.levelsUpgraded);
                }
                else
                {
                    booster.levelsUpgraded = 0;
                }
            }
            else
            {
                booster.isPurchased = false;
            }
        }
    }

    public void PurchaseBooster(BoosterSO booster)
    {
        PlayerPrefs.SetInt("BoosterPurch" + booster.boosterName, 1);
        InitBoosters();
    }

    public void EquipBooster(BoosterSO booster)
    {
        if(equippedBoosters.Count < 3)
        {
            equippedBoosters.Add(booster);
            PlayerPrefs.SetInt("BoosterEquip" + booster.boosterName, 1);
        }
        else
        {
            PlayerPrefs.SetInt("BoosterEquip" + equippedBoosters[0].boosterName, 0);
            equippedBoosters.RemoveAt(0);

            equippedBoosters.Add(booster);
            PlayerPrefs.SetInt("BoosterEquip" + booster.boosterName, 1);
        }

        InitBoosters();
    }

    public void UnEquipBooster(BoosterSO booster)
    {
        PlayerPrefs.SetInt("BoosterEquip" + booster.boosterName, 0);
        InitBoosters();
    }

    public void UpdateBoosterLvl(BoosterSO booster)
    {
        PlayerPrefs.SetInt("BoosterUpdLvls" + booster.boosterName, booster.levelsUpgraded + 1);
        InitBoosters();
    }
}
