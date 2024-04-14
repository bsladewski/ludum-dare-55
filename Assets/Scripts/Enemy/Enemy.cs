using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemySO enemySO { get; private set; }

    [SerializeField]
    private float damageGracePeriod = 0.5f;

    [SerializeField]
    private TextMeshPro problemText;

    [SerializeField]
    private GameObject destroyedEffectPrefab;

    private bool isInGracePeriod;

    private int solution;

    private Vector3 target;

    private bool hitPlayer;

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

        if (Vector3.Distance(transform.position, target) > enemySO.stopDistance && !isInGracePeriod)
        {
            Vector3 moveDir = (target - transform.position).normalized;
            transform.forward = moveDir;
            transform.position += moveDir * enemySO.moveSpeed * Time.deltaTime;
        }
        else if (!isInGracePeriod)
        {
            isInGracePeriod = true;
            StartCoroutine(Attack());
        }
    }

    public int GetSolution()
    {
        return solution;
    }

    public void DestroyEnemy()
    {
        Instantiate(destroyedEffectPrefab, transform.position, Quaternion.identity);
        EnemyManager.Instance.EnemyDestroyed(this);
        Destroy(gameObject);
    }

    public bool GetHitPlayer()
    {
        return hitPlayer;
    }

    private void GenerateProblem()
    {
        int termCount = UnityEngine.Random.Range(2, enemySO.maxTerms + 1);
        int maxSum = Mathf.Min(9999, enemySO.maxSum + (int)StateManager.Instance.GetDifficultyModifier());
        int maxTermValue = maxSum / termCount;

        int[] terms = new int[termCount];
        solution = 0;
        for (int i = 0; i < terms.Length; i++)
        {
            int termValue = UnityEngine.Random.Range(1, maxTermValue + 1);
            terms[i] = termValue;
            solution += termValue;
        }

        problemText.text = terms[0].ToString();
        for (int i = 1; i < terms.Length; i++)
        {
            problemText.text += "+" + terms[i].ToString();
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(damageGracePeriod);
        hitPlayer = true;
        Player.Instance.HitPlayer();
        DestroyEnemy();
    }
}
