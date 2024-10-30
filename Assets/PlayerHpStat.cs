using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpStat : MonoBehaviour
{
    [SerializeField] Button updatePlayerStatBtn;
    [SerializeField] GameObject maxUpdatedObject;
    [SerializeField] GameObject[] updatedObjects;

    int updateStatus;
    private void OnEnable()
    {
        RefreshPlayerHpStat();
    }

    private void RefreshPlayerHpStat()
    {
        updatePlayerStatBtn.onClick.RemoveAllListeners();

        updateStatus = PlayerPrefs.GetInt("PlayerHpStat", 0);

        for (int i = 0; i < updatedObjects.Length; i++)
        {
            updatedObjects[i].SetActive(false);
        }

        for (int i = 0; i < updateStatus; i++)
        {
            updatedObjects[i].SetActive(true);
        }

        if (updateStatus >= updatedObjects.Length)
        {
            maxUpdatedObject.gameObject.SetActive(true);
            PlayerPrefs.SetInt("PlayerHpStat", updatedObjects.Length);
        }
        else
        {
            updatePlayerStatBtn.onClick.AddListener(() => {
                PurchaseConfirmPanel.instance.EnablePurchaseConfirm("Player HP", 30, () =>
                {
                    int coins = PlayerPrefs.GetInt("TotalCoins");
                    if (coins < 30)
                        return;

                    coins -= 30;
                    PlayerPrefs.SetInt("TotalCoins", coins);
                    updateStatus++;
                    PlayerPrefs.SetInt("PlayerHpStat", updateStatus);
                    RefreshPlayerHpStat();
                });
            });
        }
    }
}