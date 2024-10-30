using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoosterPurchPrefab : MonoBehaviour
{
    [SerializeField] Image boosterIconImage;
    [SerializeField] TMP_Text boosterNameText;
    [SerializeField] TMP_Text boosterDescText;
    [SerializeField] Button buyBtn;
    [SerializeField] GameObject purchasedText;
    [SerializeField] GameObject coinsHolder;
    [SerializeField] TMP_Text coinsText;

    public void InitUI(BoosterSO booster)
    {
        boosterIconImage.sprite = booster.boosterIcon;
        boosterNameText.text = booster.boosterName;
        boosterDescText.text = booster.boosterDesc;
        coinsText.text = booster.boosterPrice.ToString();

        if(booster.isPurchased)
        {
            buyBtn.gameObject.SetActive(false);
            coinsHolder.SetActive(false);
            purchasedText.SetActive(true);
        }
        else
        {
            purchasedText.SetActive(false);
            coinsHolder.SetActive(true);

            buyBtn.gameObject.SetActive(true);
            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetInt("TotalCoins") < booster.boosterPrice)
                    return;


                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - booster.boosterPrice);
                BoostersManager.instance.PurchaseBooster(booster);

                GetComponentInParent<StoreBoostersUI>().RefreshUI();
            });
        }
    }
}
