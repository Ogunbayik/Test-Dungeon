using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float maxSpawnCount;
    [SerializeField] private Transform[] spawnPositions;

    public float spawnCount;

    private float spawnTimer;

    private bool isSpawning;

    void Start()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        spawnTimer = maxSpawnTimer;
        isSpawning = true;
        spawnCount = 0;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            var randomIndex = Random.Range(0, enemyList.Count);
            var randomEnemy = enemyList[randomIndex];

            var enemy = Instantiate(randomEnemy,spawnPositions[i]);
            spawnPositions[i].GetComponent<SpawnPoint>().IsSpawned(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (isSpawning == false)
            return;

        if (spawnCount < maxSpawnCount)
            isSpawning = true;
        else
            isSpawning = false;

        if (isSpawning)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                spawnCount++;
                spawnTimer = maxSpawnTimer;
                CreateEnemy();
            }
        }
    }

    private void CreateEnemy()
    {
        var randomIndex = Random.Range(0, enemyList.Count);
        var randomEnemy = Instantiate(enemyList[randomIndex]);

        

    }
}
