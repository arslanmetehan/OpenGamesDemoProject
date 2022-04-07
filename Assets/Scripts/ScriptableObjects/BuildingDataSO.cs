using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Building", menuName = "Building/BuildingData")]
public class BuildingDataSO : ScriptableObject
{
    [Header("Soldier")]
    public int SoldierCount;
    public int GoldCountForSolider;
    public int RockCountForSolider;
    public GameObject SoldierObject;
}
