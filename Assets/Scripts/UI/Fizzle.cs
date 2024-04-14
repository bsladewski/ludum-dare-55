using UnityEngine;

public class Fizzle : MonoBehaviour
{
    [SerializeField]
    private float jitter = 0.25f;

    [SerializeField]
    private float duration = 0.5f;

    private float timer;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (timer > duration)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
        transform.position = startPosition + UnityEngine.Random.onUnitSphere * jitter;
    }
}
