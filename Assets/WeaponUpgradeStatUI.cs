using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeStatUI : MonoBehaviour
{
    [SerializeField] List<GameObject> yellowObjs;
    [SerializeField] GameObject maxBtn;

    public void RefreshUI(int statLvl)
    {
        foreach (GameObject obj in yellowObjs)
        {
            obj.SetActive(false);
        }

        for(int i = 0; i < statLvl; i++)
        {
            yellowObjs[i].SetActive(true);
        }

        if(statLvl >= 5)
            maxBtn.SetActive(true);
        else
            maxBtn.SetActive(false);
    }
}
