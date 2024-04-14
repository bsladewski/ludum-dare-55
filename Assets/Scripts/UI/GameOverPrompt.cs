using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPrompt : MonoBehaviour
{
    public static GameOverPrompt Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton GameOverPrompt already exists!");
        }

        Instance = this;
    }

    public void Done()
    {
        SceneManager.LoadScene(0);
    }
}
