using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemySO enemySO { get; private set; }

    private float difficultyModifier;

    public void SetDifficultyModifier(float difficultyModifier)
    {
        this.difficultyModifier = difficultyModifier;
    }

    private void GenerateProblem()
    {

    }
}
