using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isSpawned;

    public void SetSpawned(bool isSpawned)
    {
        this.isSpawned = isSpawned;
    }

    public bool IsSpawned()
    {
        return isSpawned;
    }
}
