using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static Action OnLastEnemyDestroyed;

    public static Action<Enemy> OnEnemyHit;

    public static Action OnMiss;

    private void Awake()
    {
        Player.OnSolutionEntered += Player_OnSolutionEntered;
    }

    private void Start()
    {
        Enemy.OnAnyEnemyDestroyed += Enemy_OnAnyEnemyDestroyed;
    }

    private void Player_OnSolutionEntered(int solution)
    {
        int hits = 0;
        foreach (Enemy enemy in GetAllEnemies())
        {
            if (enemy.GetSolution() == solution)
            {
                hits++;
                OnEnemyHit?.Invoke(enemy);
                enemy.DestroyEnemy();
            }
        }

        if (hits == 0)
        {
            OnMiss?.Invoke();
        }
    }

    private void Enemy_OnAnyEnemyDestroyed(Enemy enemy)
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
