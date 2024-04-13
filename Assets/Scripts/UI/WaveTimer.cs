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
    }

    [SerializeField]
    private TextMeshProUGUI timerText;

    public void Enable()
    {
        timerText.gameObject.SetActive(true);
    }

    public void Disable()
    {
        timerText.gameObject.SetActive(false);
    }

    public void UpdateTimer(float timeRemaining)
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }
}
