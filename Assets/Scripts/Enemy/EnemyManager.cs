using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton EnemyManager already instantiated!");
        }

        Instance = this;
    }

    public Action OnLastEnemyDestroyed;

    public Action<Enemy> OnEnemyDestroyed;

    public Action OnMiss;

    private void Start()
    {
        Player.Instance.OnSolutionEntered += Player_OnSolutionEntered;
    }

    private void Player_OnSolutionEntered(int solution)
    {
        int hits = 0;
        foreach (Enemy enemy in GetAllEnemies())
        {
            if (enemy.GetSolution() == solution)
            {
                hits++;
                OnEnemyDestroyed?.Invoke(enemy);
                enemy.DestroyEnemy();
            }
        }

        if (hits == 0)
        {
            OnMiss?.Invoke();
        }
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        if (StateManager.Instance.GetGameState() != StateManager.GameState.BossRound)
        {
            return;
        }

        Enemy[] enemies = GetAllEnemies();
        if (enemies.Length == 0 || (enemies.Length == 1) && enemies[0].Equals(enemy))
        {
            OnLastEnemyDestroyed?.Invoke();
        }
    }

    private Enemy[] GetAllEnemies()
    {
        return FindObjectsOfType<Enemy>();
    }
}
