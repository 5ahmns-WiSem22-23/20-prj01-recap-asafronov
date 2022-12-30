using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinishArea : MonoBehaviour
{
    private Gamelogic gamelogic;
    [Header("UI-Debug stuff")]
    public Text timer;

    [Header("Gamobjects to not collide with:")]
    public float minDistance = 1f;
    public List<GameObject> objectsToAvoid;

    [Header("Collectables Settings")]
    private float elapsedTime = 0f;

    public float timeInterval;

    [Header("Collectables")]
    public GameObject collectable;
    public GameObject PowerUp1;
    public GameObject PowerUp2;
    public GameObject PowerUp3;
    public Vector2 spawnRange;

    public UnityEvent WhenInsideArea;
    public UnityEvent WhenInsideAreaAndCollected;

    public void Start()
    {
        gamelogic = FindObjectOfType<Gamelogic>();

        SpawnNewCollectable(collectable);
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        SpawnNewPowerUp(PowerUp1);

        timer.text = elapsedTime.ToString("F2");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gamelogic.canCollect == false)
        {
            WhenInsideAreaAndCollected.Invoke();
        }
    }

    public void SpawnNewCollectable(GameObject pickUp)
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

        Instantiate(pickUp, position, Quaternion.identity);
    }

    public void SpawnNewPowerUp(GameObject powerUp1)
    {
        if(elapsedTime >= timeInterval)
        { //instantiate the prefab and reset the elapsed time
            Instantiate(powerUp1, transform.position, Quaternion.identity);
            elapsedTime = elapsedTime - timeInterval;
        }
    }
}