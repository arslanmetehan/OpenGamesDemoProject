using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingDataSO buildingData;
    public Transform woodsParent;
    public Transform rocksParent;
    public Transform goldsParent;

    [HideInInspector] public int woodCount = 0;
    [HideInInspector] public int rockCount = 0;
    [HideInInspector] public int goldCount = 0;

    [HideInInspector] public bool isWoodsDone = false;
    [HideInInspector] public bool isRocksDone = false;
    [HideInInspector] public bool isGoldsDone = false;
    bool isBuildingDone = false;
    
    int SoldierCount;

    public enum Stages
    {
        Building,
        Soldier,
        CreatingSoldier
    }
    public Stages currentStage;

    private void Awake()
    {
        currentStage = Stages.Building;
    }
    public bool CheckPropCount()
    {
        if(woodCount == woodsParent.childCount)
        {
            isWoodsDone = true;
        }
        if (rockCount == rocksParent.childCount)
        {
            isRocksDone = true;
        }
        if (goldCount == goldsParent.childCount)
        {
            isGoldsDone = true;
        }
        if(isWoodsDone && isRocksDone && isGoldsDone)
        {
            isBuildingDone = true;
            currentStage = Stages.Soldier;
            GameManager.Instance.buildingManager.CompleteBuilding(transform);
            return isBuildingDone;
        }
      
        return isBuildingDone;
    }
    public void CreateSoldier(Building building)
    {
        if(currentStage == Stages.Soldier)
        {
            SoldierCount += 1;
            if(building.GetInstanceID() == GameManager.Instance.building1.GetComponent<Building>().GetInstanceID())
            {
                GameObject soldier = Instantiate(buildingData.SoldierObject, Vector3.zero, Quaternion.identity, GameManager.Instance.soldierParent);
                soldier.transform.position = GameManager.Instance.firstSoldierSpot_1;
                soldier.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                GameManager.Instance.bowerList.Add(soldier);
                GameManager.Instance.uiManager.bowerCount.text = (buildingData.SoldierCount - GameManager.Instance.bowerList.Count).ToString();

                if (GameManager.Instance.firstSoldierSpot_1.x %3 == 0)
                {
                    GameManager.Instance.firstSoldierSpot_1.z += 1.5f;
                    GameManager.Instance.firstSoldierSpot_1.x = -1;
                }
                else
                {
                    GameManager.Instance.firstSoldierSpot_1.x -= 1.5f;
                }
            }
            else
            {
                GameObject soldier = Instantiate(buildingData.SoldierObject, Vector3.zero, Quaternion.identity, GameManager.Instance.soldierParent);
                soldier.transform.position = GameManager.Instance.firstSoldierSpot_2;
                soldier.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                GameManager.Instance.spearList.Add(soldier);
                GameManager.Instance.uiManager.spearCount.text = (buildingData.SoldierCount - GameManager.Instance.spearList.Count).ToString();

                if (GameManager.Instance.firstSoldierSpot_2.x % 3 == 0)
                {
                    GameManager.Instance.firstSoldierSpot_2.z += 1.5f;
                    GameManager.Instance.firstSoldierSpot_2.x = 6;
                }
                else
                {
                    GameManager.Instance.firstSoldierSpot_2.x -= 1.5f;
                }
            }
            GameManager.Instance.HappySoliders();
            GameManager.Instance.CheckCreatedSoldierCount();
        }
    
    }
    public bool CheckSoldierCount()
    {
        if(SoldierCount >= buildingData.SoldierCount)
        {
            return false;
        }
        return true;
    }
}
