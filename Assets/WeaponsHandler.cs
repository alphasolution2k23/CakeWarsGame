using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
    #region Singleton
    public static WeaponsHandler instance;
    void Awake()
    {
        instance = this;
    }
    #endregion


    [SerializeField] List<WeaponSO> weapons;
    [SerializeField] List<WeaponSO> purchasedWeapons;
    [SerializeField] int SelectedWeaponIndex;

    public event Action<WeaponSO> onUseWeapon;

    private void Start()
    {
        InitWeapons();
    }

    private void InitWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (PlayerPrefs.GetInt("WeaponUnlocked" + i) == 1)
                purchasedWeapons.Add(new WeaponSO(weapons[i]));
        }
    }

    public WeaponSO GetWeaponById(int id)
    {
        foreach (WeaponSO weapon in purchasedWeapons)
        {
            if (weapon.weaponId == id)
                return weapon;
        }

        return null;
    }
    public WeaponSO GetCurrentWeapon()
    {
        return GetWeaponById(SelectedWeaponIndex);
    }

    public void SetSelectedWeaponIndex(int _index)
    {
        SelectedWeaponIndex = _index;
        GameManager.instance.UpdateAmmoUI(GetWeaponById(SelectedWeaponIndex));
    }

    public void UseWeapon(int WeaponId)
    {
        WeaponSO weapon = GetWeaponById(WeaponId);

        if (weapon.weaponId == 1)
            SetWeaponToDefault();
        else if (weapon.weaponId == 5)
            SetWeaponToDefault();
        else if (weapon.weaponId == 16)
            SetWeaponToDefault();
        else if (weapon.weaponId == 13)
            SetWeaponToDefault();

        Debug.Log("Weapon Id: " + WeaponId);
        if (int.TryParse(weapon.totalAmmo, out int ammo))
        {
            ammo--;
            weapon.totalAmmo = ammo.ToString();

            if (ammo <= 0)
                SetWeaponToDefault();
        }
        onUseWeapon?.Invoke(weapon);
    }

    private void SetWeaponToDefault()
    {
        Debug.Log("Weapon Selected");
        GameManager.instance.SelectWeapon("CupCake");
        GameManager.instance.OnCloseWeaponSlider();
        GameManager.instance.SetWeaponSpriteToCupcake();
        SetSelectedWeaponIndex(0);
    }

    public void UseWeaponFromSelection(int WeaponId)
    {
        WeaponSO weapon = GetWeaponById(WeaponId);
        if (int.TryParse(weapon.totalAmmo, out int ammo))
        {
            ammo--;
            weapon.totalAmmo = ammo.ToString();
        }
    }
}
