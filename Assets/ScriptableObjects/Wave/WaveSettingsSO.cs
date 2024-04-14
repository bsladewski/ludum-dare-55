using UnityEngine;

[CreateAssetMenu(menuName = "Wave Settings", fileName = "Wave Settings")]
public class WaveSettingsSO : ScriptableObject
{
    public float spawnRate = 1f;

    public float difficultyModifier = 0f;

    public EnemySO waveEndBoss;

    public SpawnWeights[] spawnWeights;

    public EnemySO GetEnemySO()
    {
        float totalSpawnWeights = 0f;
        for (int i = 0; i < spawnWeights.Length; i++)
        {
            totalSpawnWeights += spawnWeights[i].weight;
        }

        float selectedIndex = Random.value * totalSpawnWeights;
        float currentIndex = 0f;
        for (int i = 0; i < spawnWeights.Length; i++)
        {
            currentIndex += spawnWeights[i].weight;
            if (selectedIndex <= currentIndex)
            {
                return spawnWeights[i].enemySO;
            }
        }

        return spawnWeights[spawnWeights.Length - 1].enemySO;
    }

    public EnemySO GetBossEnemySO()
    {
        return waveEndBoss;
    }

    [System.Serializable]
    public class SpawnWeights
    {
        public EnemySO enemySO;

        public float weight;
    }
}
