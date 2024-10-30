using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PurchaseConfirmPanel : MonoBehaviour
{
    #region Singleton
    public static PurchaseConfirmPanel instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text purchaseConfirmText;
    [SerializeField] Button yesBtn;

    public void EnablePurchaseConfirm(string updateObject, int price, UnityAction purchaseBtnClick)
    {
        purchaseConfirmText.text = "Are you sure you want to update " + updateObject + " for " + price + " coins";
        panel.SetActive(true);

        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(purchaseBtnClick);
        yesBtn.onClick.AddListener(() => {
            panel.SetActive(false);
        });
    }
}