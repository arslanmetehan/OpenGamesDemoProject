using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    [Header("Managers")]
    public SOManager _soManager;
    public BuildingManager buildingManager;
    public UIManager uiManager;

    [Header("Building")]
    public Transform building1Parent;
    public Transform building2Parent;
    public Building building1;
    public Building building2;
    public Vector3 firstSoldierSpot_1 = new Vector3(-1,0,3);
    public Vector3 firstSoldierSpot_2 = new Vector3(6,0,3);

    [Header("BackPack")]
    public Transform backPack;
    public float nextSlotPosY = 0;

    [Header("Pools")]
    public WoodPool woodPool;
    public RockPool rockPool;
    public GoldPool goldPool;

    [Header("Created Soliders")]
    public List<GameObject> bowerList;
    public List<GameObject> spearList;
    public Transform soldierParent;


    public int nextLevel = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

    }
    private void OnEnable()
    {
        Instance = this;
    }
    public void PrepareNextLevel()
    {
     
        uiManager.EndOfLevelAnimations();
    }
    public void GetNextLevel()
    {
        firstSoldierSpot_1 = new Vector3(-1, 0, 3);
        firstSoldierSpot_2 = new Vector3(6, 0, 3);
        bowerList.Clear();
        spearList.Clear();
        uiManager.FadeInOutScreen();
        buildingManager.GetBuildFromLevel(_soManager.buildingDataByLevel[nextLevel-1]);
        uiManager.SetUpMineCounts();
    }
    public void CheckCreatedSoldierCount()
    {
        if((bowerList.Count + spearList.Count) >= (building1.buildingData.SoldierCount + building2.buildingData.SoldierCount))
        {
            PrepareNextLevel();
        }
    }
    public void HappySoliders()
    {
        for(int i = 0; i < bowerList.Count; i++)
        {
            bowerList[i].GetComponent<Animator>().SetTrigger("Win");
        }
        for (int i = 0; i < spearList.Count; i++)
        {
            spearList[i].GetComponent<Animator>().SetTrigger("Win");
        }
    }
}
