using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class FinishArea : MonoBehaviour
{
    private Gamelogic gamelogic;

    [Header("Gamobjects to not collide with:")]
    public float minDistance = 1f;
    public List<GameObject> objectsToAvoid;

    [Header("Collectables")]
    public GameObject collectable;
    public Vector2 spawnRange;

    public UnityEvent WhenInsideArea;
    public UnityEvent WhenInsideAreaAndCollected;

    public void Start()
    {
        gamelogic = FindObjectOfType<Gamelogic>();

        SpawnNewCollectable();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gamelogic.canCollect == false)
        {
            WhenInsideAreaAndCollected.Invoke();
        }
    }

    public void SpawnNewCollectable()
    {
        var position = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), 0f);

        while (true)
        {
            // Check if the instantiated object intersects with any of the objects in the list
            bool intersects = false;
            foreach (GameObject obj in objectsToAvoid)
            {
                if (Vector2.Distance(position, obj.transform.position) < minDistance)
                {
                    intersects = true;
                    break;
                }
            }

            if (!intersects) break;

            position = new Vector2(Random.Range(-spawnRange.x / 2, spawnRange.x / 2), Random.Range(-spawnRange.y / 2, spawnRange.y / 2));
        }

        Instantiate(collectable, position, Quaternion.identity);
    }
}