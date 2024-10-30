using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Monkey,
    Gorilla,
    Chimpanzee,
    AcrobatMonkey
}

public class GameEventsHandler : MonoBehaviour
{
    #region Singleton
    public static GameEventsHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public Action<EnemyType> onKillEnemy;

    private void Start()
    {
        onKillEnemy += (enemyType) =>
        {
            Debug.Log("Killed Enemy of Type: " + enemyType);
        };
    }
}
