using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIPrefab : MonoBehaviour
{
    [SerializeField] TMP_Text questInfo;
    [SerializeField] TMP_Text questPrizeAmount;
    [SerializeField] TMP_Text questDone;
    [SerializeField] Button collectBtn;

    public void RefreshPrefabUI(DailyQuest quest)
    {
        questInfo.text = quest.questInfo;
        questPrizeAmount.text = quest.questPrizeAmount.ToString();

        if(quest.isClaimed)
        {
            Destroy(gameObject);
        }
        else
        {
            if(quest.completedTasks >= quest.totalTasks)
            {
                collectBtn.gameObject.SetActive(true);
                questDone.gameObject.SetActive(false);
                collectBtn.interactable = true;

                collectBtn.onClick.AddListener(() => {
                    PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + quest.questPrizeAmount);
                    MenuController.menuControllerInst.RefreshUI();
                    quest.SetIsClaimed(true);

                    RefreshPrefabUI(quest);
                    GetComponentInParent<DailyQuestUI>().RefreshDailyQuestsUI();
                });
            }
            else
            {
                collectBtn.gameObject.SetActive(false);
                questDone.gameObject.SetActive(true);
                questDone.text = quest.completedTasks.ToString() + "/" + quest.totalTasks.ToString();
            }
        }
    }
}
