using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour
{
    public static WaveTimer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singelton WaveTimer already instantiated!");
        }

        Instance = this;
        Player.OnSolutionEntered += Player_OnSolutionEntered;
    }

    [SerializeField]
    private GameObject contents;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private GameObject helpText;

    public void Enable()
    {
        contents.SetActive(true);
        helpText.SetActive(true);
    }

    public void Disable()
    {
        contents.SetActive(false);
        helpText.SetActive(false);
    }

    public void UpdateTimer(float timeRemaining)
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    private void Player_OnSolutionEntered(int solution)
    {
        helpText.SetActive(false);
    }
}
