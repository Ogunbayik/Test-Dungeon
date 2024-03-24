using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool isSpawned;
    void Start()
    {
        isSpawned = false;
    }

    public void IsSpawned(bool isSpawned)
    {
        this.isSpawned = isSpawned;
    }
}
