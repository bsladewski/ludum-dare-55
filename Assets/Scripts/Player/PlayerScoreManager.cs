using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton PlayerScoreManager already instantiated!");
        }

        Instance = this;
    }

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public int score { get; private set; }

    private void Start()
    {
        StateManager.Instance.OnGameStateChanged += StateManager_OnGameStateChanged;
        EnemyManager.Instance.OnEnemyDestroyed += EnemyManager_OnEnemyDestroyed;
        UpdateScoreText();
    }

    private void StateManager_OnGameStateChanged(StateManager.GameState gameState)
    {
        if (gameState != StateManager.GameState.GameStart && gameState != StateManager.GameState.GameOver)
        {
            scoreText.gameObject.SetActive(true);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    private void EnemyManager_OnEnemyDestroyed(Enemy enemy)
    {
        if (!enemy.GetHitPlayer())
        {
            score += enemy.enemySO.pointValue;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }
}
