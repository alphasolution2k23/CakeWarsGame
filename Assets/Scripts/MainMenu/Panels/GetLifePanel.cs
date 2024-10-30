using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AliScripts.Utils;

public class GetLifePanel : MonoBehaviour
{
    [SerializeField] Button AdBtn;
    [SerializeField] Button CoinsBtn;
    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text info1Text;
    [SerializeField] TMP_Text info2Text;
    [SerializeField] TMP_Text info3Text;

    [SerializeField] int coinsPrice;
    [SerializeField] GameObject purchaseLifeStuff;

    private void OnEnable()
    {

        Debug.Log("Current Heath: " + PlayerPrefs.GetInt("CurrentHealth").ToString());
        AdBtn.onClick.RemoveAllListeners();
        AdBtn.onClick.AddListener(() => {
            Action onAdComplete = () => {
                PlayerPrefs.SetInt("CurrentHealth", LivesRestorer.instance.DefHealth);
            };
            AdManager.instance.ShowRewardedVideo(onAdComplete);
        });

        CoinsBtn.onClick.RemoveAllListeners();
        CoinsBtn.onClick.AddListener(() => {
            int coins = PlayerPrefs.GetInt("TotalCoins");
            if (coins < coinsPrice)
                return;
            int currentHealth = PlayerPrefs.GetInt("CurrentHealth");
            if (currentHealth >= LivesRestorer.instance.DefHealth)
                return;

            coins -= coinsPrice;
            PlayerPrefs.SetInt("CurrentHealth", LivesRestorer.instance.DefHealth);
            PlayerPrefs.SetInt("TotalCoins", coins);
        });

        coinsText.text = coinsPrice.ToString();
    }

    private void Update()
    {

        int currentHealth = PlayerPrefs.GetInt("CurrentHealth");

        if(currentHealth < 5 && currentHealth > 0)
        {
            info2Text.text = "You have " + currentHealth + " of 5 lives";
            info3Text.gameObject.SetActive(false);
            info1Text.gameObject.SetActive(true);
        }
        else if(currentHealth == 5)
        {
            info2Text.text = "You have all 5 lives";
            info3Text.text = "Each defeat will cost you one life. A new life regenerates every 20 mins";
            info3Text.gameObject.SetActive(true);
            info1Text.gameObject.SetActive(false);
            purchaseLifeStuff.SetActive(false);
        }
        else if(currentHealth == 0)
        {
            info2Text.text = "You are out of lives";
            info3Text.gameObject.SetActive(false);
            info1Text.gameObject.SetActive(true);
        }

        if (currentHealth < LivesRestorer.instance.DefHealth)
        {
            purchaseLifeStuff.SetActive(true);
            info1Text.text = currentHealth + " of 5 lives, Next Life in " + LivesRestorer.instance.timeLeft.Minutes.ToString("D2") + ":" + LivesRestorer.instance.timeLeft.Seconds.ToString("D2");
        }
    }
}
