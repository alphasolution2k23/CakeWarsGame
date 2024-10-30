using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoostersUI : MonoBehaviour
{
    [SerializeField] GameObject boosterPrefab;
    [SerializeField] GameObject noBoostersMessage;
    [SerializeField] Transform boostersHolder;


    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform t in boostersHolder)
            Destroy(t.gameObject);

        foreach (BoosterSO booster in BoostersManager.instance.boosters)
        {
            GameObject prefab = Instantiate(boosterPrefab, boostersHolder);
            BoosterEquipPrefab prefabScript = prefab.GetComponent<BoosterEquipPrefab>();
            prefabScript.InitUI(booster);
        }

        noBoostersMessage.SetActive(true);

        foreach (BoosterSO booster in BoostersManager.instance.boosters)
        {
            if (booster.isPurchased)
            {
                noBoostersMessage.SetActive(false);
                break;
            }
        }

    }
}
