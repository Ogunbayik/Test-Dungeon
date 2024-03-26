using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public enum SpawnType
    {
        Infinity,
        Finite
    }

    public SpawnType spawnType;

    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float maxSpawnCount;
    [SerializeField] private Transform[] spawnPositions;

    public float spawnCount;

    private float spawnTimer;

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

        StartSpawning();
    }

    private void StartSpawning()
    {
        spawnTimer = maxSpawnTimer;
        spawnCount = 0;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i].GetComponent<SpawnPoint>().SetSpawned(true);

            var randomIndex = Random.Range(0, enemyList.Count);
            var randomEnemy = enemyList[randomIndex];
            var enemy = Instantiate(randomEnemy,spawnPositions[i]);
            
            spawnCount++;
        }
    }

    void Update()
    {
        switch(spawnType)
        {
            case SpawnType.Infinity:
                InfinitySpawn();
                break;
            case SpawnType.Finite:
                FiniteSpawn();
                break;
        }
    }

    private void InfinitySpawn()
    {
        foreach (var spawnPos in spawnPositions)
        {
            var spawnPoint = spawnPos.GetComponent<SpawnPoint>();

            if (spawnPos.childCount > 0)
                spawnPoint.SetSpawned(true);
            else
                spawnPoint.SetSpawned(false);

            if (spawnPoint.IsSpawned() == false)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer <= 0)
                {
                    spawnTimer = maxSpawnTimer;

                    CreateRandomEnemy(spawnPos);
                    spawnPoint.SetSpawned(true);
                }
            }
        }
    }

    private void FiniteSpawn()
    {
        foreach (var spawnPosition in spawnPositions)
        {
            var spawnPoint = spawnPosition.GetComponent<SpawnPoint>();
            spawnPoint.SetSpawned(true);
        }
    }

    private void CreateRandomEnemy(Transform spawnPosition)
    {
        var randomIndex = Random.Range(0, enemyList.Count);
        var randomEnemy = Instantiate(enemyList[randomIndex], spawnPosition);
        randomEnemy.transform.position = spawnPosition.position;
    }
}
