using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostersGameplayUI : MonoBehaviour
{
    [SerializeField] Button boostersOpener;
    [SerializeField] GameObject boostersHolderObject;

    private void Start()
    {
        boostersOpener.onClick.RemoveAllListeners();
        boostersOpener.onClick.AddListener(() => {
            boostersHolderObject.SetActive(!boostersHolderObject.activeInHierarchy);
        });
    }
}
