using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthVisual playerHealthVisualPrefab;

    private PlayerHealthVisual[] healthVisuals;

    public void UpdateHealth(int maxHealth, int currentHealth)
    {
        if (healthVisuals == null || healthVisuals.Length != maxHealth)
        {
            InitializeHealthVisuals(maxHealth);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthVisuals[i].SetFull();
            }
            else
            {
                healthVisuals[i].SetEmpty();
            }
        }
    }

    private void InitializeHealthVisuals(int maxHealth)
    {
        if (healthVisuals != null && healthVisuals.Length != maxHealth)
        {
            for (int i = 0; i < maxHealth; i++)
            {
                Destroy(healthVisuals[i].gameObject);
            }

            healthVisuals = null;
        }

        if (healthVisuals == null)
        {
            healthVisuals = new PlayerHealthVisual[maxHealth];
            for (int i = 0; i < maxHealth; i++)
            {
                PlayerHealthVisual playerHealthVisual = Instantiate(playerHealthVisualPrefab);
                playerHealthVisual.transform.SetParent(transform);
                healthVisuals[i] = playerHealthVisual;
            }
        }
    }
}
