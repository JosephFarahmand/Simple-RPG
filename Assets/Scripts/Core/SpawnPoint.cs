using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int maxEnemyCount = 5;
    private int currentEnemyCount = 0;

    private void Start()
    {
        GameManager.Spawner.AddPoint(this);
    }

    public Enemy SpawnEnemy(Enemy enemyPrefab)
    {
        if (currentEnemyCount > maxEnemyCount) return null;

        var enemy = Instantiate(enemyPrefab, transform);
        enemy.GetComponent<CharacterStats>().OnDie += Stats_OnDie;

        currentEnemyCount++;

        return enemy;
    }

    private void Stats_OnDie()
    {
        currentEnemyCount--;
    }
}
