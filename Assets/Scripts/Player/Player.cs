using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    public static Action<int> OnSolutionEntered;

    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private TextMeshProUGUI solutionText;

    private PlayerControls playerControls;

    private int solution = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Player._0.started += (_) => OnNumberInput(0);
        playerControls.Player._1.started += (_) => OnNumberInput(1);
        playerControls.Player._2.started += (_) => OnNumberInput(2);
        playerControls.Player._3.started += (_) => OnNumberInput(3);
        playerControls.Player._4.started += (_) => OnNumberInput(4);
        playerControls.Player._5.started += (_) => OnNumberInput(5);
        playerControls.Player._6.started += (_) => OnNumberInput(6);
        playerControls.Player._7.started += (_) => OnNumberInput(7);
        playerControls.Player._8.started += (_) => OnNumberInput(8);
        playerControls.Player._9.started += (_) => OnNumberInput(9);

        playerControls.Player.Enter.started += (_) => OnEnterPressed();
        playerControls.Player.Backspace.started += (_) => OnBackspacePressed();
    }

    private void Start()
    {
        UpdateSolution();
        Enemy.OnEnemyAttackPlayer += Enemy_OnEnemyAttackPlayer;
        StateManager.Instance.OnGameStateChanged += StateManager_OnGameStateChanged;

        currentHealth = maxHealth;
        playerHealth.UpdateHealth(maxHealth, currentHealth);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private bool PlayerInputAllowed()
    {
        StateManager.GameState gameState = StateManager.Instance.GetGameState();
        return gameState == StateManager.GameState.WaveInProgress ||
               gameState == StateManager.GameState.BossRound;
    }

    private void OnNumberInput(int number)
    {
        if (!PlayerInputAllowed()) return;

        int newSolution = solution * 10 + number;
        if (newSolution > 999)
        {
            Debug.Log("Solution too large!");
            return;
        }

        solution = newSolution;
        UpdateSolution();
    }

    private void OnEnterPressed()
    {
        if (!PlayerInputAllowed()) return;

        if (solution > 0)
        {
            OnSolutionEntered?.Invoke(solution);
        }

        solution = 0;
        UpdateSolution();
    }

    private void OnBackspacePressed()
    {
        if (!PlayerInputAllowed()) return;

        solution /= 10;
        UpdateSolution();
    }

    private void UpdateSolution()
    {
        if (solution == 0)
        {
            solutionText.gameObject.SetActive(false);
            return;
        }

        solutionText.gameObject.SetActive(true);
        solutionText.text = solution.ToString();
    }

    private void Enemy_OnEnemyAttackPlayer()
    {
        currentHealth -= 1;
        playerHealth.UpdateHealth(maxHealth, currentHealth);
    }

    private void StateManager_OnGameStateChanged(StateManager.GameState gameState)
    {
        switch (gameState)
        {
            case StateManager.GameState.WaveInProgress:
            case StateManager.GameState.BossRound:
                playerHealth.gameObject.SetActive(true);
                break;
            default:
                playerHealth.gameObject.SetActive(false);
                break;
        }
    }
}
