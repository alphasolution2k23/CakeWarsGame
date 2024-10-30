using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyQuestUI : MonoBehaviour
{

    [SerializeField] Transform questsHolder;
    [SerializeField] GameObject questPrefab;
    [SerializeField] GameObject allMissionsCompletedText;

    [SerializeField] TMP_Text refreshInText;
    DateTime refreshTime;
    private void OnEnable()
    {
        refreshTime = DailyQuestsManager.instance.refreshTime; 
        RefreshDailyQuestsUI();
    }

    public void RefreshDailyQuestsUI()
    {
        DestroyAllChildren();

        DailyQuest dailyQuest1 = DailyQuestsManager.instance.dailyQuest1.GetComponent<DailyQuest>();
        DailyQuest dailyQuest2 = DailyQuestsManager.instance.dailyQuest2.GetComponent<DailyQuest>();
        DailyQuest dailyQuest3 = DailyQuestsManager.instance.dailyQuest3.GetComponent<DailyQuest>();

        GameObject _questPrefab;
        QuestUIPrefab _prefabScript;

        _questPrefab = Instantiate(questPrefab, questsHolder);
        _prefabScript = _questPrefab.GetComponent<QuestUIPrefab>();
        _prefabScript.RefreshPrefabUI(dailyQuest1);

        _questPrefab = Instantiate(questPrefab, questsHolder);
        _prefabScript = _questPrefab.GetComponent<QuestUIPrefab>();
        _prefabScript.RefreshPrefabUI(dailyQuest2);

        _questPrefab = Instantiate(questPrefab, questsHolder);
        _prefabScript = _questPrefab.GetComponent<QuestUIPrefab>();
        _prefabScript.RefreshPrefabUI(dailyQuest3);

        if(dailyQuest1.isClaimed && dailyQuest2.isClaimed&& dailyQuest3.isClaimed)
            allMissionsCompletedText.SetActive(true);
        else
            allMissionsCompletedText.SetActive(false);
    }

    private void Update()
    {
        TimeSpan timeLeft = refreshTime - DateTime.Now;
        if (timeLeft >= new TimeSpan(0, 0, 0))
            refreshInText.text = "Refresh in " + timeLeft.Hours + "hr " + timeLeft.Minutes + "m " + timeLeft.Seconds + "s";
    }

    private void DestroyAllChildren()
    {
        foreach (Transform t in questsHolder)
            Destroy(t.gameObject);
    }
}
