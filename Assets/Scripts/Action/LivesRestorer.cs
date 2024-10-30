using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesRestorer : MonoBehaviour
{
    DateTime lastLifeRestoreTime;
    [SerializeField] public int DefHealth = 5;
    public TimeSpan timeLeft;

    #region Singleton
    public static LivesRestorer instance;
    private void Awake()
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

    private void Start()
    {
        string lastDateTimeString = PlayerPrefs.GetString("lastLifeRestoreTime", "");

        if (string.IsNullOrEmpty(lastDateTimeString))
            lastDateTimeString = DateTime.MinValue.ToString();

        lastLifeRestoreTime = DateTime.Parse(lastDateTimeString);
    }

    int livesToRetore;
    int minutesPassed;
    private void Update()
    {
        DateTime nextLifeLineTime = lastLifeRestoreTime.AddMinutes(20);

        timeLeft = nextLifeLineTime - DateTime.Now;

        if (timeLeft <= new TimeSpan(0, 0, 0))
        {
            livesToRetore = 1;
            minutesPassed = Mathf.Abs((int)timeLeft.TotalMinutes);
            while (minutesPassed >= 20)
            {
                livesToRetore++;
                minutesPassed -= 20;
            }

            PlayerPrefs.SetString("lastLifeRestoreTime", DateTime.Now.ToString());
            lastLifeRestoreTime = DateTime.Now;

            int currentHealth = PlayerPrefs.GetInt("CurrentHealth");
            if (currentHealth >= DefHealth)
            {
                return;
            }

            int newHealth = Mathf.Min(currentHealth + livesToRetore, DefHealth);
            PlayerPrefs.SetInt("CurrentHealth", newHealth);
        }
    }
}
