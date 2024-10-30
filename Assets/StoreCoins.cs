using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreCoins : MonoBehaviour
{
    [SerializeField] TMP_Text totalCoinsText;

    void Update()
    {
        totalCoinsText.text = "Total Coins: " + PlayerPrefs.GetInt("TotalCoins").ToString();
    }
}
