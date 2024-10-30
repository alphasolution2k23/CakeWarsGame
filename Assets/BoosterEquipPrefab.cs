using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterEquipPrefab : MonoBehaviour
{
    [SerializeField] Image boosterIconImage;
    [SerializeField] TMP_Text boosterNameText;
    [SerializeField] TMP_Text boosterDescText;
    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;
    [SerializeField] GameObject[] boosterUpdates;
    [SerializeField] Button updateBtn;
    [SerializeField] GameObject maxNoUpdateBtn;

    public void InitUI(BoosterSO booster)
    {
        if (!booster.isPurchased)
            Destroy(gameObject);

        boosterIconImage.sprite = booster.boosterIcon;
        boosterNameText.text = booster.boosterName;
        boosterDescText.text = booster.boosterDesc;
        equipBtn.gameObject.SetActive(false);
        unEquipBtn.gameObject.SetActive(false);

        if (booster.isEquipped)
        {
            unEquipBtn.onClick.RemoveAllListeners();
            unEquipBtn.onClick.AddListener(() => {
                BoostersManager.instance.UnEquipBooster(booster);
                GetComponentInParent<UpgradeBoostersUI>().RefreshUI();
            });
            unEquipBtn.gameObject.SetActive(true);
        }
        else
        {
            equipBtn.onClick.RemoveAllListeners();
            equipBtn.onClick.AddListener(() => {
                BoostersManager.instance.EquipBooster(booster);
                GetComponentInParent<UpgradeBoostersUI>().RefreshUI();
            });
            equipBtn.gameObject.SetActive(true);
        }


        //Updating Booster Levels
        for (int i = 0; i < booster.levelsUpgraded; i++)
        {
            boosterUpdates[i].SetActive(true);
        }

        if(booster.levelsUpgraded >= boosterUpdates.Length)
        {
            updateBtn.gameObject.SetActive(false);
            maxNoUpdateBtn.gameObject.SetActive(true);
        }
        else
        {
            updateBtn.gameObject.SetActive(true);
            maxNoUpdateBtn.gameObject.SetActive(false);

            updateBtn.onClick.RemoveAllListeners();
            updateBtn.onClick.AddListener(() => {

                PurchaseConfirmPanel.instance.EnablePurchaseConfirm(booster.boosterName, 30, () => {
                    Debug.Log("Updating Booster");
                    if (PlayerPrefs.GetInt("TotalCoins") < 30)
                        return;

                    BoostersManager.instance.UpdateBoosterLvl(booster);
                    Debug.Log("Updated Booster");
                    GetComponentInParent<UpgradeBoostersUI>().RefreshUI();
                });
            });

        }
    }
}
