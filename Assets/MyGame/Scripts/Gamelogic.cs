using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable
{
    public static bool canCollect = true;
}

public class Gamelogic : MonoBehaviour
{
    [Header("Settings for collectables")]
    public int collected;

    [Space]

    [Header("UI")]
    public TMP_Text collectedNumbers;

    [Space]

    [Header("Spawnpoints for enemies")]
    public string enemySpawnPoint = "SpawnPoints";
    [Space]
    public GameObject[] enemies;
    [Space]
    public Transform[] enemySpawnPoints;
    [Space]
    private GameObject[] spawnPointObjects;
    private bool checker = false;

    [Space]

    [Header("Settings for collectables")]
    public Vector2 spawnRange;
    public float minDistance;
    public GameObject collectable;
    public GameObject[] powerUps = new GameObject[3];
    [Space]
    public List<GameObject> objectsToAvoid;

    private void Start()
    {
        SpawnNewCollectable(collectable);

        spawnPointObjects = GameObject.FindGameObjectsWithTag(enemySpawnPoint);
        enemySpawnPoints = new Transform[spawnPointObjects.Length];

        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            enemySpawnPoints[i] = spawnPointObjects[i].transform;
        }
    }

    private void Update()
    {
        if (collected % 2 == 0 && !checker)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            var enemyInstantiate = enemies[randomIndex];
            SpawnNewEnemy(enemyInstantiate);
            checker = true;
        }

        if (collected % 2 == 1) checker = false;

        collectedNumbers.text = collected.ToString();
    }

    //for the Unityevent to invoke this Method.
    public void AddCollectable()
    {
        if(Collectable.canCollect) collected++;
    }

    public void MakeCollectTrue()
    {
        Collectable.canCollect = true;
    }

    public void MakeCollectFalse()
    {
        Collectable.canCollect = false;
    }

    public void SpawnNewEnemy(GameObject enemy)
    {
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Transform spawnPoint = enemySpawnPoints[randomIndex];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); 
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
        //if (elapsedTime >= timeInterval)
        //{ //instantiate the prefab and reset the elapsed time
         //   Instantiate(powerUp1, transform.position, Quaternion.identity);
         //   elapsedTime = elapsedTime - timeInterval;
        //}
    }
}
