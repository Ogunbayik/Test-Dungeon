using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;

    [SerializeField] private float maxSpawnCount;

    private float spawnCount;
    void Start()
    {
        spawnCount = 0;
        enemyList = new List<GameObject>();

        while (spawnCount < maxSpawnCount)
        {
            Debug.Log("Spawned");
            spawnCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
