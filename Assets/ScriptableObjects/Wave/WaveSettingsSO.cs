using UnityEngine;

[CreateAssetMenu(menuName = "Wave Settings", fileName = "Wave Settings")]
public class WaveSettingsSO : ScriptableObject
{
    public float spawnRate = 1f;

    public float difficultyModifier = 1f;

    public EnemySO waveEndBoss;

    public SpawnWeights[] spawnWeights;

    [System.Serializable]
    public class SpawnWeights
    {
        public EnemySO enemySO;

        public float weight;
    }
}
