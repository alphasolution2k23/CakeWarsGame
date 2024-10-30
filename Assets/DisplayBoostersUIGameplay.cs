using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBoostersUIGameplay : MonoBehaviour
{
    [SerializeField] BoosterGameplayDisplayPrefab booster1;
    [SerializeField] BoosterGameplayDisplayPrefab booster2;
    [SerializeField] BoosterGameplayDisplayPrefab booster3;

    private void Start()
    {
        List<BoosterSO> boosters = BoostersManager.instance.equippedBoosters;

        if(boosters.Count <= 0)
        {
            booster1.gameObject.SetActive(false);
            booster2.gameObject.SetActive(false);
            booster3.gameObject.SetActive(false);
        }
        else if (boosters.Count == 1)
        {
            booster1.gameObject.SetActive(true);
            booster2.gameObject.SetActive(false);
            booster3.gameObject.SetActive(false);

            booster1.InitUI(boosters[0]);
        }
        else if (boosters.Count == 2)
        {
            booster1.gameObject.SetActive(true);
            booster2.gameObject.SetActive(true);
            booster3.gameObject.SetActive(false);

            booster1.InitUI(boosters[0]);
            booster2.InitUI(boosters[1]);
        }
        else if (boosters.Count == 3)
        {
            booster1.gameObject.SetActive(true);
            booster2.gameObject.SetActive(true);
            booster3.gameObject.SetActive(true);

            booster1.InitUI(boosters[0]);
            booster2.InitUI(boosters[1]);
            booster3.InitUI(boosters[2]);
        }
    }
}