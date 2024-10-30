using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterGameplayDisplayPrefab : MonoBehaviour
{
    [SerializeField] Image boosterIconImage;
    Button boosterBtn;

    private void Start()
    {
        boosterBtn = GetComponent<Button>();
        boosterBtn.onClick.RemoveAllListeners();
        boosterBtn.onClick.AddListener(() => {
            boosterBtn.interactable = false;
        });
    }

    public void InitUI(BoosterSO booster)
    {
        boosterIconImage.sprite = booster.boosterIcon;
    }
}
