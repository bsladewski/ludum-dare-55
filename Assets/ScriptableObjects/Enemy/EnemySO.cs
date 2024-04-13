using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxTerms = 2;

    public int maxSum = 40;

    public int pointValue = 2;

    public float moveSpeed = 2f;

    public Enemy enemyPrefab;
}
