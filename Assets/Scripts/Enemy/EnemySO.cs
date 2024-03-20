using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int enemyDamage;
    public int maxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float moveRange;
    public Sprite sprite;
    public float maxWaitTimer;
    public float chaseDistance;
}
