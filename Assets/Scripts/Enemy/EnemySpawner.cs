using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton EnemySpawner already instantiated!");
        }

        Instance = this;
    }

    [SerializeField]
    private float spawnRadius = 10f;

    [SerializeField]
    private float spawnFuzzFactor = 2f;

    private static readonly float SPAWN_TIMER_GRACE_PERIOD = 2f;

    private float spawnTimer = SPAWN_TIMER_GRACE_PERIOD;

    private void Start()
    {
        StateManager.Instance.OnGameStateChanged += StateManager_OnGameStateChanged;
    }

    private void Update()
    {
        if (StateManager.Instance.GetGameState() != StateManager.GameState.WaveInProgress)
        {
            return;
        }

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

    public void ResetEnemySpawner()
    {
        spawnTimer = SPAWN_TIMER_GRACE_PERIOD;
    }

    private void SpawnEnemy()
    {
        WaveSettingsSO waveSettings = StateManager.Instance.GetWaveSettings();
        EnemySO enemy = waveSettings.GetEnemySO();
        SpawnEnemy(enemy.enemyPrefab);
    }

    private void StateManager_OnGameStateChanged(StateManager.GameState gameState)
    {
        if (gameState != StateManager.GameState.BossRound)
        {
            return;
        }

        WaveSettingsSO waveSettings = StateManager.Instance.GetWaveSettings();
        SpawnEnemy(waveSettings.GetBossEnemySO().enemyPrefab);
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        float spawnDistance = spawnRadius + (Random.value * spawnFuzzFactor);
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(enemyPrefab, new Vector3(spawnPoint.x, 0f, spawnPoint.y), Quaternion.identity);
    }
}
