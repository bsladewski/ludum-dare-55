using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnTimer = 2f;

    private void Update()
    {
        if (spawnTimer < 0f)
        {
            SpawnEnemy();
            spawnTimer = StateManager.Instance.GetWaveSettings().spawnRate;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        // TODO:
    }
}
