using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEnemyAttackPlayer;

    [field: SerializeField]
    public EnemySO enemySO { get; private set; }

    private float difficultyModifier;

    private int solution;

    private void Awake()
    {
        GenerateProblem();
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
}
