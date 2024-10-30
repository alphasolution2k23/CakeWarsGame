using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DailyQuestsManager : MonoBehaviour
{
    #region Singleton
    public static DailyQuestsManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] List<GameObject> dailyQuestPrefabs;
    private Dictionary<int, GameObject> dailyQuestsRefId;

    public GameObject dailyQuest1;
    public GameObject dailyQuest2;
    public GameObject dailyQuest3;
    public DateTime refreshTime;

    private void Start()
    {
        FillDailyQuestsDictionary();
        LoadDailyQuests();
    }

    private void FillDailyQuestsDictionary()
    {
        dailyQuestsRefId = new Dictionary<int, GameObject>();
        foreach (var i in dailyQuestPrefabs)
        {
            dailyQuestsRefId.Add(i.GetComponent<DailyQuest>().questId, i);
        }
    }

    private void LoadDailyQuests()
    {
        string lastLoadedPath = Path.Combine(Application.persistentDataPath, "LastLoadedDailyQuests.txt");

        if (File.Exists(lastLoadedPath))
        {
            DateTime lastLoadedDate;
            //File.WriteAllText(lastLoadedPath, DateTime.Today.ToString());

            string data = File.ReadAllText(lastLoadedPath);
            lastLoadedDate = DateTime.Parse(data);
            refreshTime = lastLoadedDate.AddDays(1);

            TimeSpan timeFromLastLoadedDate = DateTime.Today - lastLoadedDate;

            if (timeFromLastLoadedDate.Days > 0)
            {
                GetNewQuests();
            }
            else
            {
                LoadCurrentQuests();
            }
        }
        else
        {
            refreshTime = DateTime.Today.AddDays(1);
            GetNewQuests();
        }
    }

    private void LoadCurrentQuests()
    {
        Debug.Log("Load Current Quests");
        string quest1Path = Path.Combine(Application.persistentDataPath, "quest1.json");
        string quest2Path = Path.Combine(Application.persistentDataPath, "quest2.json");
        string quest3Path = Path.Combine(Application.persistentDataPath, "quest3.json");

        DailyQuest quest1 = new DailyQuest();
        DailyQuest quest2 = new DailyQuest();
        DailyQuest quest3 = new DailyQuest();

        quest1.LoadDailyQuest(quest1Path);
        quest2.LoadDailyQuest(quest2Path);
        quest3.LoadDailyQuest(quest3Path);

        dailyQuest1 = Instantiate(dailyQuestsRefId[quest1.questId], Vector3.zero, Quaternion.identity);
        dailyQuest2 = Instantiate(dailyQuestsRefId[quest2.questId], Vector3.zero, Quaternion.identity);
        dailyQuest3 = Instantiate(dailyQuestsRefId[quest3.questId], Vector3.zero, Quaternion.identity);

        dailyQuest1.GetComponent<DailyQuest>().SetLoadedValues(quest1);
        dailyQuest2.GetComponent<DailyQuest>().SetLoadedValues(quest2);
        dailyQuest3.GetComponent<DailyQuest>().SetLoadedValues(quest3);
    }

    private void GetNewQuests()
    {
        Debug.Log("Load new Quests");
        string quest1Path = Path.Combine(Application.persistentDataPath, "quest1.json");
        string quest2Path = Path.Combine(Application.persistentDataPath, "quest2.json");
        string quest3Path = Path.Combine(Application.persistentDataPath, "quest3.json");

        List<GameObject> tempList = dailyQuestPrefabs;
        int index;

        index = UnityEngine.Random.Range(0, tempList.Count);
        DailyQuest quest1 = tempList[index].GetComponent<DailyQuest>();
        quest1.SaveDaliyQuest(quest1Path);
        dailyQuest1 = Instantiate(dailyQuestsRefId[quest1.questId], Vector3.zero, Quaternion.identity);
        tempList.RemoveAt(index);

        index = UnityEngine.Random.Range(0, tempList.Count);
        DailyQuest quest2 = tempList[index].GetComponent<DailyQuest>();
        quest2.SaveDaliyQuest(quest2Path);
        dailyQuest2 = Instantiate(dailyQuestsRefId[quest2.questId], Vector3.zero, Quaternion.identity);
        tempList.RemoveAt(index);

        index = UnityEngine.Random.Range(0, tempList.Count);
        DailyQuest quest3 = tempList[index].GetComponent<DailyQuest>();
        quest3.SaveDaliyQuest(quest3Path);
        dailyQuest3 = Instantiate(dailyQuestsRefId[quest3.questId], Vector3.zero, Quaternion.identity);
        tempList.RemoveAt(index);


        string lastLoadedPath = Path.Combine(Application.persistentDataPath, "LastLoadedDailyQuests.txt");
        File.WriteAllText(lastLoadedPath, DateTime.Today.ToString());
    }
}