using System;
using UnityEngine;

public class GameStartPrompt : MonoBehaviour
{
    public static GameStartPrompt Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton GameStartPrompt already exists!");
        }

        Instance = this;
    }

    public Action OnGameStarted;

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        gameObject.SetActive(false);
    }
}
