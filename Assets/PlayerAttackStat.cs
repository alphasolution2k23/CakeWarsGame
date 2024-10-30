using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackStat : MonoBehaviour
{
    [SerializeField] Button updatePlayerHealthStatBtn;
    [SerializeField] GameObject maxUpdatedObject;
    [SerializeField] GameObject[] updatedObjects;

    int updateStatus;
    private void OnEnable()
    {
        RefreshPlayerHpStat();
    }

    private void RefreshPlayerHpStat()
    {
        updatePlayerHealthStatBtn.onClick.RemoveAllListeners();

        updateStatus = PlayerPrefs.GetInt("PlayerAttackStat", 0);

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
            PlayerPrefs.SetInt("PlayerAttackStat", updatedObjects.Length);
        }
        else
        {
            updatePlayerHealthStatBtn.onClick.AddListener(() => {
                PurchaseConfirmPanel.instance.EnablePurchaseConfirm("Player Attack", 30, () =>
                {
                    int coins = PlayerPrefs.GetInt("TotalCoins");
                    if (coins < 30)
                        return;

                    coins -= 30;
                    PlayerPrefs.SetInt("TotalCoins", coins);
                    updateStatus++;
                    PlayerPrefs.SetInt("PlayerAttackStat", updateStatus);
                    RefreshPlayerHpStat();
                });
            });
        }
    }
}
