using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class WaveCountdownVisual : MonoBehaviour
{
    public static WaveCountdownVisual Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singelton WaveCountdownVisual already instantiated!");
        }

        Instance = this;
    }

    public Action OnCountdownEnded;

    [SerializeField]
    private GameObject contents;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    [SerializeField]
    private float startBuffer = 1f;

    [SerializeField]
    private float countdownDuration = 5f;

    private float countdownTimer;

    private bool isCountingDown;

    public void Initialize(int currentWave, bool shouldDelay)
    {
        titleText.text = string.Format("Wave {0}", currentWave);
        countdownTimer = countdownDuration;
        if (shouldDelay)
        {
            StartCoroutine(BufferCoroutine());
        }
        else
        {
            BeginCountdown();
        }
    }

    private void Update()
    {
        if (!isCountingDown) return;

        countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
        countdownTimer -= Time.deltaTime;
        if (countdownTimer < 0)
        {
            isCountingDown = false;
            contents.SetActive(false);
            OnCountdownEnded?.Invoke();
        }
    }

    private IEnumerator BufferCoroutine()
    {
        yield return new WaitForSeconds(startBuffer);
        BeginCountdown();
    }

    private void BeginCountdown()
    {
        contents.SetActive(true);
        isCountingDown = true;
    }
}
