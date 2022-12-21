using UnityEngine;
using UnityEngine.Events;

public class FinishArea : MonoBehaviour
{
    public Vector2 range;

    [Header("Collectables")]
    public GameObject collectable;

    public UnityEvent WhenInsideArea;

    public void Start()
    {
        var col = Instantiate(collectable);
        col.transform.position = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WhenInsideArea.Invoke();
        }
    }

    public void SpawnNewCollectable()
    {
        var col = Instantiate(collectable);
        col.transform.position = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0f);
    }
}