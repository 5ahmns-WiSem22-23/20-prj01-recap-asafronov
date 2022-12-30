using UnityEngine;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
    [Header("Settings for collectables")]
    public int collected;
    public bool canCollect = true;

    [Space]

    [Header("UI")]
    public Text collectedNumbers;

    [Space]

    [Header("Spawnpoints for enemies")]
    public string enemySpawnPoint = "SpawnPoints";
    [Space]
    public GameObject[] enemies;
    [Space]
    public Transform[] enemySpawnPoints;
    [Space]
    private GameObject[] spawnPointObjects;

    private void Start()
    {
        spawnPointObjects = GameObject.FindGameObjectsWithTag(enemySpawnPoint);
        enemySpawnPoints = new Transform[spawnPointObjects.Length];

        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            enemySpawnPoints[i] = spawnPointObjects[i].transform;
        }
    }

    private void Update()
    {
        collectedNumbers.text = collected.ToString();

        if (collected == 1)
        {
            Debug.Log("Should spawn Enemy");
            int randomIndex = Random.Range(0, enemies.Length);
            var enemyyy = enemies[randomIndex];
            SpawnNewEnemy(enemyyy);
            return;
        }
    }

    //for the Unityevent to invoke this Method.
    public void addCollectable()
    {
        if(canCollect) collected++;
    }

    public void MakeCollectTrue()
    {
        canCollect = true;
    }

    public void MakeCollectFalse()
    {
        canCollect = false;
    }

    public void SpawnNewEnemy(GameObject enemy)
    {
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Transform spawnPoint = enemySpawnPoints[randomIndex];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); 
    }
}
