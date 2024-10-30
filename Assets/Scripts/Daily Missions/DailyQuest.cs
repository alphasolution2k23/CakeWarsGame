using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DailyQuest : MonoBehaviour
{
    [TextArea]
    public string questInfo;
    public int questId;
    public int totalTasks;
    public int completedTasks;
    public bool isClaimed;
    public int questPrizeAmount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region FileManagement
    public void SaveDaliyQuest(string fileName)
    {
        try
        {
            DailyQuestSerializeAble quest = new DailyQuestSerializeAble
            {
                _questInfo = questInfo,
                _totalTasks = totalTasks,
                _completedTasks = completedTasks,
                _questId = questId,
                _isClaimed = isClaimed,
                _questPrizeAmount = questPrizeAmount
            };

            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonUtility.ToJson(quest);

            File.WriteAllText(filePath, json);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void LoadDailyQuest(string fileName)
    {

        try
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            string json = File.ReadAllText(filePath);

            DailyQuestSerializeAble obj = JsonUtility.FromJson<DailyQuestSerializeAble>(json);

            questInfo = obj._questInfo;
            totalTasks = obj._totalTasks;
            completedTasks = obj._completedTasks;
            questId = obj._questId;
            isClaimed = obj._isClaimed;
            questPrizeAmount= obj._questPrizeAmount;
        }
        catch (Exception exc)
        {
            Debug.LogError(exc);
        }
    }

    public void SetLoadedValues(DailyQuest loadedQuest)
    {
        questInfo = loadedQuest.questInfo;
        totalTasks = loadedQuest.totalTasks;
        completedTasks = loadedQuest.completedTasks;
        questId = loadedQuest.questId;
        isClaimed = loadedQuest.isClaimed;
    }

    public string LoadFilePath()
    {

        string filePath = "";
        if (this.gameObject == DailyQuestsManager.instance.dailyQuest1)
        {
            filePath = Path.Combine(Application.persistentDataPath, "quest1.json");
        }
        else if (this.gameObject == DailyQuestsManager.instance.dailyQuest2)
        {
            filePath = Path.Combine(Application.persistentDataPath, "quest2.json");
        }
        else if (this.gameObject == DailyQuestsManager.instance.dailyQuest3)
        {
            filePath = Path.Combine(Application.persistentDataPath, "quest3.json");
        }

        return filePath;
    }

    #endregion

    public void CompleteTask()
    {
        completedTasks++;
        if (completedTasks > totalTasks)
            completedTasks = totalTasks;

        SaveDaliyQuest(LoadFilePath());
    }

    public void SetIsClaimed(bool _isClaimed)
    {
        isClaimed = _isClaimed;
        SaveDaliyQuest(LoadFilePath());
    }
}

[System.Serializable]
class DailyQuestSerializeAble
{
    public string _questInfo;
    public int _questId;
    public int _totalTasks;
    public int _completedTasks;
    public bool _isClaimed;
    public int _questPrizeAmount;
}