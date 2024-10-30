using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillNEnemiesQuest : DailyQuest
{
    public EnemyType enemyType;

    private void Start()
    {
        GameEventsHandler.Instance.onKillEnemy += OnKillEnemy;
    }

    private void OnDestroy()
    {
        GameEventsHandler.Instance.onKillEnemy -= OnKillEnemy;
    }

    private void OnKillEnemy(EnemyType type)
    {
        if (type == enemyType)
            CompleteTask();
    }
}
