using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData")]
public class LevelDatSO : ScriptableObject
{
    [Header("Data")]
    public GameObject building1;
    public GameObject building2;
}
