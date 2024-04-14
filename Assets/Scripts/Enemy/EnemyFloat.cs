using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    [SerializeField]
    private float bobSpeed = 4f;

    [SerializeField]
    private float bobHeight = 0.2f;

    private float phaseOffset;

    private void Start()
    {
        phaseOffset = UnityEngine.Random.Range(0f, 10f);
    }

    private void Update()
    {
        float height = Mathf.Sin((Time.time + phaseOffset) * bobSpeed) * bobHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
    }
}
