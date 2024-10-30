using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    #region Singleton
    public static WeaponSelectionUI instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] TMP_Text weaponNameText;
    [SerializeField] TMP_Text weaponAmmoAmountText;
    [SerializeField] Image weaponImage;
    [SerializeField] Button[] equipBtn;

    public void UpdateUI(WeaponSO weapon)
    {
        weaponNameText.text = weapon.weaponName;
        weaponImage.sprite = weapon.weaponImage;

        string AmmoText = "";
        int AmmoAmount;
        if (weapon.totalAmmo == "N/A")
        {
            AmmoText = "∞";
            AmmoAmount = 10000;
        }
        else
        {
            AmmoText = weapon.totalAmmo + " Left";
            AmmoAmount = int.Parse(weapon.totalAmmo);
        }

        weaponAmmoAmountText.text = AmmoText;

        if(AmmoAmount <= 0)
        {
            DisableEquipBtns();
        }
        else
        {
            EnableEquipBtn(weapon.weaponId);
        }
    }


    void DisableEquipBtns()
    {
        foreach (var btn in equipBtn)
        {
            btn.gameObject.SetActive(false);
        }
    }
    void EnableEquipBtn(int weaponId)
    {
        DisableEquipBtns();
        equipBtn[weaponId].gameObject.SetActive(true);
    }

}
