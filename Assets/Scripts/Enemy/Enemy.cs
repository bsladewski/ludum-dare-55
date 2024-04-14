using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEnemyAttackPlayer;

    [field: SerializeField]
    public EnemySO enemySO { get; private set; }

    [SerializeField]
    private float damageGracePeriod = 0.5f;

    private bool isInGracePeriod;

    private float difficultyModifier;

    private int solution;

    private Vector3 target;

    private void Awake()
    {
        GenerateProblem();
    }

    private void Start()
    {
        target = Player.Instance.transform.position;
    }

    private void Update()
    {
        StateManager.GameState gameState = StateManager.Instance.GetGameState();
        if (gameState != StateManager.GameState.WaveInProgress && gameState != StateManager.GameState.BossRound)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target) > 1.5f && !isInGracePeriod)
        {
            Vector3 moveDir = (target - transform.position).normalized;
            transform.Translate(moveDir * enemySO.moveSpeed * Time.deltaTime);
        }
        else if (!isInGracePeriod)
        {
            isInGracePeriod = true;
            StartCoroutine(Attack());
        }
    }

    public void SetDifficultyModifier(float difficultyModifier)
    {
        this.difficultyModifier = difficultyModifier;
        GenerateProblem();
    }

    public int GetSolution()
    {
        return solution;
    }

    private void GenerateProblem()
    {
        solution = 0; // TODO:
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(damageGracePeriod);
        OnEnemyAttackPlayer?.Invoke();
        Destroy(gameObject);
    }
}
