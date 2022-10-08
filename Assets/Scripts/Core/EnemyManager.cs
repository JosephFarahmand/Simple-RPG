using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<SpawnPoint> spawnPoints;

    [SerializeField, Min(0)] private int toleranceTime = 5;
    [SerializeField, Min(2)] private int interval = 5;
    float timePassed;
    bool canPlay = true;
    int Interval;

    List<Enemy> enemies;

    private void Start()
    {
        Interval = Random.Range(interval, interval + toleranceTime);
        enemies = new List<Enemy>();
        spawnPoints = new List<SpawnPoint>();
    }

    private void Update()
    {
        if (!GameManager.IsRun) return;
        timePassed += Time.deltaTime;
        if (canPlay && (int)timePassed % Interval == 0)
        {
            canPlay = false;
            SpawnEnemy();
        }
        else if ((int)timePassed % Interval == 1)
        {
            canPlay = true;
        }
    }

    private void SpawnEnemy()
    {
        var point = spawnPoints.RandomItem();
        var enemyPrefab = GameManager.GameData.GetRandomEnemy();

        var spawnedEnemy = point.SpawnEnemy(enemyPrefab);

        if (spawnedEnemy != null)
        {
            enemies.Add(spawnedEnemy);
        }
    }

    public void AddPoint(SpawnPoint point)
    {
        spawnPoints.Add(point);
    }
}
