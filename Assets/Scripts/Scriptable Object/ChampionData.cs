using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewChampionStats", menuName = "Game/Champion Stats")]
public class ChampionData : ScriptableObject
{
    public ChampionName name;
    public int maxHp;
    public int attackPower;
    public int armor;
    public float attackRange;
    public float attackSpeed;
    public float movementSpeed;

    public void Init()
    {
        movementSpeed *= 0.5f;
        attackRange *= 0.02857f;
    }
}
