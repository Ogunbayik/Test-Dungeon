using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationZero : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
