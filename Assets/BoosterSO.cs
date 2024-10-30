using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Booster", menuName = "ScriptableObjects/Boosters", order = 1)]
public class BoosterSO : ScriptableObject
{
    public Sprite boosterIcon;
    [Tooltip("it also is id so two boosters should not have same name")]
    public string boosterName;
    public string boosterDesc;
    public int boosterPrice;

    [Header("Dont Change")]
    public bool isPurchased;
    public bool isEquipped;
    public int levelsUpgraded;
}
