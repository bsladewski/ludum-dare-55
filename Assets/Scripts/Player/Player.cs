using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Action OnPlayerDeath;

    public Action<int> OnSolutionEntered;

    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private TextMeshProUGUI solutionText;

    [SerializeField]
    private GameObject solutionHelpText;

    [SerializeField]
    private PlayerAttack playerAttackPrefab;

    [SerializeField]
    private GameObject playerHitEffectPrefab;

    [SerializeField]
    private Transform lightningSpawnPoint;

    [SerializeField]
    private Transform playerVisual;

    private PlayerControls playerControls;

    private int solution = 0;

    private float rotationCooldown = 0.2f;

    private float rotationTimer;

    private Vector3 startLocalScale;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton Player already exists!");
        }

        Instance = this;

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        UpdateSolution();
        StateManager.Instance.OnGameStateChanged += StateManager_OnGameStateChanged;
        EnemyManager.Instance.OnEnemyDestroyed += EnemyManager_OnEnemyDestroyed;

        currentHealth = maxHealth;
        playerHealth.UpdateHealth(maxHealth, currentHealth);
        startLocalScale = playerVisual.localScale;
    }

    private void _0(InputAction.CallbackContext ctx) { OnNumberInput(0); }
    private void _1(InputAction.CallbackContext ctx) { OnNumberInput(1); }
    private void _2(InputAction.CallbackContext ctx) { OnNumberInput(2); }
    private void _3(InputAction.CallbackContext ctx) { OnNumberInput(3); }
    private void _4(InputAction.CallbackContext ctx) { OnNumberInput(4); }
    private void _5(InputAction.CallbackContext ctx) { OnNumberInput(5); }
    private void _6(InputAction.CallbackContext ctx) { OnNumberInput(6); }
    private void _7(InputAction.CallbackContext ctx) { OnNumberInput(7); }
    private void _8(InputAction.CallbackContext ctx) { OnNumberInput(8); }
    private void _9(InputAction.CallbackContext ctx) { OnNumberInput(9); }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Player._0.started += _0;
        playerControls.Player._1.started += _1;
        playerControls.Player._2.started += _2;
        playerControls.Player._3.started += _3;
        playerControls.Player._4.started += _4;
        playerControls.Player._5.started += _5;
        playerControls.Player._6.started += _6;
        playerControls.Player._7.started += _7;
        playerControls.Player._8.started += _8;
        playerControls.Player._9.started += _9;

        playerControls.Player.Enter.started += OnEnterPressed;
        playerControls.Player.Backspace.started += OnBackspacePressed;
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Player._0.started -= _0;
        playerControls.Player._1.started -= _1;
        playerControls.Player._2.started -= _2;
        playerControls.Player._3.started -= _3;
        playerControls.Player._4.started -= _4;
        playerControls.Player._5.started -= _5;
        playerControls.Player._6.started -= _6;
        playerControls.Player._7.started -= _7;
        playerControls.Player._8.started -= _8;
        playerControls.Player._9.started -= _9;

        playerControls.Player.Enter.started -= OnEnterPressed;
        playerControls.Player.Backspace.started -= OnBackspacePressed;
    }

    private void Update()
    {
        if (rotationTimer > 0f)
        {
            rotationTimer -= Time.deltaTime;
        }

        float squash = Mathf.Sin(Time.time) * 0.2f;
        playerVisual.localScale = new Vector3(
            startLocalScale.x - squash / 2f,
            startLocalScale.y + squash,
            startLocalScale.z - squash / 2f
        );
    }

    private bool PlayerInputAllowed()
    {
        StateManager.GameState gameState = StateManager.Instance.GetGameState();
        return gameState == StateManager.GameState.WaveInProgress ||
               gameState == StateManager.GameState.BossRound;
    }

    private void OnNumberInput(int number)
    {
        if (!PlayerInputAllowed())
        {
            solution = 0;
            UpdateSolution();
            return;
        }

        int newSolution = solution * 10 + number;
        if (newSolution > 9999)
        {
            solutionHelpText.SetActive(true);
            return;
        }

        solution = newSolution;
        UpdateSolution();
    }

    private void OnEnterPressed(InputAction.CallbackContext ctx)
    {
        if (!PlayerInputAllowed()) return;

        if (solution > 0)
        {
            solutionHelpText.SetActive(false);
            OnSolutionEntered?.Invoke(solution);
        }

        solution = 0;
        UpdateSolution();
    }

    private void OnBackspacePressed(InputAction.CallbackContext ctx)
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

    public void HitPlayer()
    {
        Instantiate(playerHitEffectPrefab, transform.position, Quaternion.identity);
        currentHealth -= 1;
        playerHealth.UpdateHealth(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
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

    private void EnemyManager_OnEnemyDestroyed(Enemy enemy)
    {
        if (rotationTimer <= 0f)
        {
            transform.forward = (enemy.transform.position - transform.position).normalized;
            rotationTimer = rotationCooldown;
        }

        PlayerAttack playerAttack = Instantiate(playerAttackPrefab);
        playerAttack.Initialize(
            lightningSpawnPoint.position,
            enemy.transform.position + Vector3.up
        );
    }
}
