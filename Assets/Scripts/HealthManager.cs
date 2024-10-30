using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    private const string LastExitTimeKey = "LastExitTime";

    public int currentHealth;
    public float healthPerMinute = 0.17f;

 

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("CurrentHealth");
        CalculateHealthRefill();
    }

    void OnApplicationQuit()
    {
        SaveExitTime();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveExitTime();
        }
    }

    void SaveExitTime()
    {
        DateTime currentTime = DateTime.Now;
        PlayerPrefs.SetString(LastExitTimeKey, currentTime.ToString());
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.Save();
    }

    void CalculateHealthRefill()
    {
        if (PlayerPrefs.HasKey(LastExitTimeKey))
        {
            string lastExitTimeStr = PlayerPrefs.GetString(LastExitTimeKey);
            DateTime lastExitTime = DateTime.Parse(lastExitTimeStr);
            TimeSpan timeElapsed = DateTime.Now - lastExitTime;

            int minutesElapsed = (int)timeElapsed.TotalMinutes;
            if (minutesElapsed >= 20)
            {
                PlayerPrefs.SetInt("CurrentHealth", PlayerPrefs.GetInt("CurrentHealth")+1);
            }
        }
    }
}