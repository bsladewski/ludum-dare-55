using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    public Action<GameState> OnGameStateChanged;

    [SerializeField]
    private GameStartPrompt gameStartPrompt;

    [SerializeField]
    private GameOverPrompt gameOverPrompt;

    [SerializeField]
    private WaveSettingsSO[] waveSettings;

    [SerializeField]
    private int currentWave = 1;

    [SerializeField]
    private float endGameDifficultyRamp = 1f;

    [SerializeField]
    private float waveDuration = 120f;

    private float waveTimer;

    private GameState gameState = GameState.GameStart;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton StateManager already exists!");
        }

        Instance = this;
    }

    private void Start()
    {
        gameStartPrompt.gameObject.SetActive(true);
        GameStartPrompt.Instance.OnGameStarted += GameStartPrompt_OnGameStarted;
        WaveCountdownVisual.Instance.OnCountdownEnded += WaveCountdown_OnCountdownEnded;
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaveInProgress:
                HandleWaveInProgress();
                break;
            default:
                break;
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public WaveSettingsSO GetWaveSettings()
    {
        return waveSettings[Mathf.Min(waveSettings.Length - 1, currentWave - 1)];
    }

    public float GetDifficultyModifier()
    {
        WaveSettingsSO waveSettingsSO = GetWaveSettings();
        int excessWaves = currentWave - waveSettings.Length;
        excessWaves = Mathf.Max(0, excessWaves);
        return waveSettingsSO.difficultyModifier + excessWaves * endGameDifficultyRamp;
    }

    private void InitWaveCountdown()
    {
        WaveCountdownVisual.Instance.Initialize(currentWave, false);
    }

    private void WaveCountdown_OnCountdownEnded()
    {
        InitWaveInProgress();
        SetGameState(GameState.WaveInProgress);
    }

    private void InitWaveInProgress()
    {
        waveTimer = waveDuration;
        EnemySpawner.Instance.ResetEnemySpawner();
        WaveTimer.Instance.UpdateTimer(waveTimer);
        WaveTimer.Instance.Enable();
    }

    private void HandleWaveInProgress()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer < 0f)
        {
            InitBossRound();
            SetGameState(GameState.BossRound);
        }
        else
        {
            WaveTimer.Instance.UpdateTimer(waveTimer);
        }
    }

    private void InitBossRound()
    {
        Debug.Log("Boss round!");
    }

    private void InitGameOver()
    {
        gameOverPrompt.gameObject.SetActive(true);
    }

    private void Player_OnPlayerDeath()
    {
        InitGameOver();
        SetGameState(GameState.GameOver);
    }

    private void GameStartPrompt_OnGameStarted()
    {
        InitWaveCountdown();
        SetGameState(GameState.WaveCountdown);
    }

    private void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    public enum GameState
    {
        GameStart,
        WaveCountdown,
        WaveInProgress,
        BossRound,
        WaveOver,
        GameOver
    }
}
