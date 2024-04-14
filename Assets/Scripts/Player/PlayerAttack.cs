using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private float displayTime = 2f;

    private float displayTimer;

    private void Start()
    {
        displayTimer = displayTime;
    }

    private void Update()
    {
        if (displayTimer < 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            displayTimer -= Time.deltaTime;
        }
    }

    public void Initialize(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { start, end });
    }
}
