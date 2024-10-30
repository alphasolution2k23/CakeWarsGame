using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public WeaponSO(WeaponSO _copyData)
    {
        weaponName = _copyData.weaponName;
        description = _copyData.description;
        totalAmmo = _copyData.totalAmmo;
        ammoPerReload = _copyData.ammoPerReload;
        weaponId = _copyData.weaponId;
        weaponImage = _copyData.weaponImage;
    }

    public string weaponName;
    [TextArea()]
    public string description;
    public Sprite weaponImage;
    public string totalAmmo;
    public string ammoPerReload;
    public int weaponId;
}
