using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Collectable
{
    public static bool canCollect = true;
}

public class Gamelogic : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text collectedNumbers;
    public TMP_Text timeText;
    public GameObject pauseMenu;

    private bool timeStopped = false;
    private PlayerMovementRotation player;

    [Space]

    [Header("Settings for collectables")]
    public int collected;
    public Vector2 spawnRange;
    public float minDistance;
    public GameObject collectable;
    public GameObject[] powerUps = new GameObject[3];
    [Space]
    public List<GameObject> objectsToAvoid;

    private float elapsedTime;
    private float time;
    public float controllTime;

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


    private void Start()
    {
        Time.timeScale = 0f;

        player = FindObjectOfType<PlayerMovementRotation>();

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
        time = Time.time;
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60);
        int milliseconds = (int)(time * 1000) % 1000;

        timeText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";

        elapsedTime += Time.deltaTime;

        if (collected % 2 == 0 && !checker)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            var enemyInstantiate = enemies[randomIndex];
            SpawnNewEnemy(enemyInstantiate);
            checker = true;
        }

        if (collected % 2 == 1) checker = false;

        collectedNumbers.text = collected.ToString();

        if(elapsedTime >= controllTime)
        {
            var randomIndex = Random.Range(0, 3);
            SpawnNewCollectable(powerUps[randomIndex]);
            elapsedTime -= controllTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timeStopped = !timeStopped;
            Time.timeScale = timeStopped ? 0f : 1f;
            player.GetComponent<Rigidbody2D>().bodyType = timeStopped ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;

            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
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

            if (!intersects)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); // 0.5f is the radius of the overlap circle
                if (colliders.Length == 0)
                {
                    break;
                }
            }
            position = new Vector2(Random.Range(-spawnRange.x / 2, spawnRange.x / 2), Random.Range(-spawnRange.y / 2, spawnRange.y / 2));
        }

        Instantiate(pickUp, position, Quaternion.identity);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TimeScale()
    {
        Time.timeScale = 1f;
    }
}
