using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBoostersUI : MonoBehaviour
{
    [SerializeField] GameObject boosterPrefab;
    [SerializeField] Transform boostersHolder;


    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach(Transform t in boostersHolder)
            Destroy(t.gameObject);

        foreach(BoosterSO booster in BoostersManager.instance.boosters)
        {
            GameObject prefab = Instantiate(boosterPrefab, boostersHolder);
            BoosterPurchPrefab prefabScript = prefab.GetComponent<BoosterPurchPrefab>();
            prefabScript.InitUI(booster);
        }
    }
}
