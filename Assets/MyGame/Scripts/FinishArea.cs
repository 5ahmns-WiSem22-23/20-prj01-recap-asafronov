using UnityEngine;
using UnityEngine.Events;

public class FinishArea : MonoBehaviour
{
    public int count = 0;
    public int finalCount = 0;

    [Space]
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

        if (collision.CompareTag("Collectable") && finalCount > count)
        {
            count++;
            var col = Instantiate(collectable);
            col.transform.position = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0f);
        }
    }
}