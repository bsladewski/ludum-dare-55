using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPrompt : MonoBehaviour
{
    public static GameOverPrompt Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton GameOverPrompt already exists!");
        }

        Instance = this;
    }

    [SerializeField]
    private GameObject contents;

    [SerializeField]
    private TextMeshProUGUI resultsText;

    private void Start()
    {
        StateManager.Instance.OnGameStateChanged += StateManager_OnGameStateChanged;
    }

    public void Done()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StateManager_OnGameStateChanged(StateManager.GameState gameState)
    {
        if (gameState != StateManager.GameState.GameOver)
        {
            return;
        }

        int score = PlayerScoreManager.Instance.score;
        int highScore = PlayerPrefs.GetInt("highScore", 0);

        resultsText.text = string.Format("Thank you for playing!\n\nYour score was {0}!\n\n", score.ToString());
        if (score > highScore)
        {
            resultsText.text += "This is your new high score!";
            PlayerPrefs.SetInt("highScore", score);
        }
        else if (highScore >= score)
        {
            resultsText.text += string.Format("Your current high score is {0}!", highScore);
        }

        contents.SetActive(true);
    }
}
