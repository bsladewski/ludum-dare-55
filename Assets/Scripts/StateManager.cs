using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

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
        GameStartPrompt.Instance.OnGameStarted += GameStartPrompt_OnGameStarted;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaveCountdown:
                HandleWaveCountdown();
                break;
            case GameState.WaveInProgress:
                HandleWaveInProgress();
                break;
            case GameState.BossRound:
                HandleBossRound();
                break;
            case GameState.GameOver:
                HandleGameOver();
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
        Debug.Log("Init wave countdown.");
    }

    private void HandleWaveCountdown()
    {

    }

    private void InitWaveInProgress()
    {
        waveTimer = waveDuration;
    }

    private void HandleWaveInProgress()
    {

    }

    private void InitBossRound()
    {

    }

    private void HandleBossRound()
    {

    }

    private void InitGameOver()
    {

    }

    private void HandleGameOver()
    {

    }

    private void GameStartPrompt_OnGameStarted()
    {
        InitWaveCountdown();
        gameState = GameState.WaveCountdown;
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
