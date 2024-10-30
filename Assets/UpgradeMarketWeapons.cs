using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMarketWeapons : MonoBehaviour
{
    [SerializeField] private Sprite[] WeaponImage;
    [SerializeField] private string[] WeaponNames;
    [SerializeField] public TextMeshProUGUI WeaponName;
    [SerializeField] private Image WeaponSprite;
    [SerializeField] private GameObject LeftBtn;
    [SerializeField] private GameObject RightBtn;
    [SerializeField] private int WeaponIndex;
    [SerializeField] UpgradesBarWeaponsObject stats;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        MenuController.menuControllerInst.SetUpgradingName(WeaponNames[WeaponIndex]);
        stats.RefreshUI(WeaponNames[WeaponIndex]);
    }

    public void LeftBtnWeaponSelect()
    {
        if (WeaponIndex > 0)
        {
            WeaponIndex--;

            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) != 1)
            {
                LeftBtnWeaponSelect();
                return;
            }

            RightBtn.GetComponent<Button>().interactable = true;
            WeaponSprite.sprite = WeaponImage[WeaponIndex];
            WeaponName.text = WeaponNames[WeaponIndex].ToString();
            MenuController.menuControllerInst.SetUpgradingName(WeaponNames[WeaponIndex]);
            MenuController.menuControllerInst.OnEnableCheckUpgrade();
            stats.RefreshUI(WeaponNames[WeaponIndex]);
        }
        else
        {
            WeaponIndex = WeaponImage.Length;
            LeftBtnWeaponSelect();
        }
    }
    public void RightBtnWeaponSelect()
    {
        if (WeaponIndex < WeaponImage.Length - 1)
        {
            WeaponIndex++;

            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) != 1)
            {
                RightBtnWeaponSelect();
                return;
            }

            LeftBtn.GetComponent<Button>().interactable = true;
            WeaponSprite.sprite = WeaponImage[WeaponIndex];
            WeaponName.text = WeaponNames[WeaponIndex].ToString();
            MenuController.menuControllerInst.SetUpgradingName(WeaponNames[WeaponIndex]);
            MenuController.menuControllerInst.OnEnableCheckUpgrade();
            stats.RefreshUI(WeaponNames[WeaponIndex]);
        }
        else
        {
            WeaponIndex = -1;
            RightBtnWeaponSelect();
        }
    }
}
