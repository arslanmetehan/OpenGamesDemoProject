using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Mining")]
    public Transform backPack;
    List<Transform> propsAtBackpack = new List<Transform>();
    List<Transform> usedGolds = new List<Transform>();
    List<Transform> usedWoods = new List<Transform>();
    List<Transform> usedRocks = new List<Transform>();

    List<Transform> goldsAtBackPack = new List<Transform>();
    List<Transform> woodsAtBackPack = new List<Transform>();
    List<Transform> rocksAtBackPack = new List<Transform>();

    [Header("Mining Values")]
    int backPackLimit = 15;
    float miningRate = 0.2f;
    float soliderRate = 1f;
    float nextTimeToMining;
    float nextTimeToGetSolider;

    public Transform refundPoint;
   
    private void OnTriggerStay(Collider other)
    {
        if(Time.time >= nextTimeToMining && GameManager.Instance.backPack.childCount < backPackLimit)
        {
            nextTimeToMining = Time.time + miningRate;

            if (other.CompareTag("GoldMine"))
            {
                GameObject prop =  GameManager.Instance.goldPool.GetGoldPrefab();
                propsAtBackpack.Add(prop.transform);
                goldsAtBackPack.Add(prop.transform);
            }
            else if (other.CompareTag("TreeMine"))
            {
                GameObject prop = GameManager.Instance.woodPool.GetWoodPrefab();
                propsAtBackpack.Add(prop.transform);
                woodsAtBackPack.Add(prop.transform);
            }
            else if (other.CompareTag("RockMine"))
            {
                GameObject prop = GameManager.Instance.rockPool.GetRockPrefab();
                propsAtBackpack.Add(prop.transform);
                rocksAtBackPack.Add(prop.transform);
            }

        }
        if(other.CompareTag("Building1"))
        {
            if(Time.time >= nextTimeToGetSolider)
            {
                nextTimeToGetSolider = Time.time + soliderRate;
                if (GameManager.Instance.building1.currentStage == Building.Stages.Soldier)
                {
                    TryGetSoldier(GameManager.Instance.building1);
                }
            }

        }
        else if(other.CompareTag("Building2"))
        {
            if (Time.time >= nextTimeToGetSolider)
            {
                nextTimeToGetSolider = Time.time + soliderRate;
                if (GameManager.Instance.building2.currentStage == Building.Stages.Soldier)
                {
                    TryGetSoldier(GameManager.Instance.building2);
                }
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building1"))
        {
            if(GameManager.Instance.building1.currentStage == Building.Stages.Building)
            {
                SetBuildingPiece(GameManager.Instance.building1);
            }
      
        }
        else if (other.CompareTag("Building2"))
        {
            if(GameManager.Instance.building2.currentStage == Building.Stages.Building)
            {
                SetBuildingPiece(GameManager.Instance.building2);
            }
            
        }
        else if(other.CompareTag("Refund"))
        {
            RefundProps();
        }
    }
    void TryGetSoldier(Building building)
    {

        if(building.CheckSoldierCount())
        {

            if(goldsAtBackPack.Count >= building.buildingData.GoldCountForSolider && rocksAtBackPack.Count >= building.buildingData.RockCountForSolider)
            {
                
                for (int i = goldsAtBackPack.Count -1; i >= goldsAtBackPack.Count- building.buildingData.GoldCountForSolider; i--)
                {
                    propsAtBackpack.Remove(goldsAtBackPack[i]);

                    usedGolds.Add(goldsAtBackPack[i]);
                    goldsAtBackPack[i].parent = GameManager.Instance.goldPool.transform;
                }
                for (int i = rocksAtBackPack.Count - 1; i >= rocksAtBackPack.Count - building.buildingData.RockCountForSolider; i--)
                {
                    propsAtBackpack.Remove(rocksAtBackPack[i]);
                    usedRocks.Add(rocksAtBackPack[i]);
                    rocksAtBackPack[i].parent = GameManager.Instance.rockPool.transform;
                }
                PropAnimations(building.transform);
                StopCoroutine(nameof(RearrangeBackPack));
                StartCoroutine(nameof(RearrangeBackPack));
                //RearrangeBackpack();
                building.CreateSoldier(building);
            }
           
        }
    }
    void SetBuildingPiece(Building building)
    {
        if (!building.CheckPropCount())
        {
            int propCount = GameManager.Instance.backPack.childCount;
            float delayCountForPieces = 0;
            for (int i = propCount - 1; i >= 0; i--)
            {

                GameObject prop;
                prop = GameManager.Instance.backPack.GetChild(i).gameObject;
                if (prop.layer == 7 && !building.isWoodsDone)
                {
                    propsAtBackpack.Remove(prop.transform);
                    woodsAtBackPack.Remove(prop.transform);
                    usedWoods.Add(prop.transform);
   
                    building.woodsParent.GetChild(building.woodCount).DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(delayCountForPieces);
                    delayCountForPieces += 0.05f;
                    building.woodCount += 1;
                    building.CheckPropCount();

                    if(building.GetInstanceID() == GameManager.Instance.building1.GetInstanceID())
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b1_WoodCount,building.woodsParent, building.woodCount);
                    }
                    else
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b2_WoodCount, building.woodsParent, building.woodCount);
                    }
                   

                    prop.transform.parent = GameManager.Instance.woodPool.transform;    

                }
                else if (prop.layer == 8 && !building.isGoldsDone)
                {
                    propsAtBackpack.Remove(prop.transform);
                    goldsAtBackPack.Remove(prop.transform);
                    usedGolds.Add(prop.transform);

                    building.goldsParent.GetChild(building.goldCount).DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(delayCountForPieces);
                    delayCountForPieces += 0.05f;
                    building.goldCount += 1;
                    building.CheckPropCount();

                    if (building.GetInstanceID() == GameManager.Instance.building1.GetInstanceID())
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b1_GoldCount, building.goldsParent, building.goldCount);
                    }
                    else
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b2_GoldCount, building.goldsParent, building.goldCount);
                    }

                    prop.transform.parent = GameManager.Instance.goldPool.transform;
                }
                else if (prop.layer == 9 && !building.isRocksDone)
                {
                    propsAtBackpack.Remove(prop.transform);
                    rocksAtBackPack.Remove(prop.transform);
                    usedRocks.Add(prop.transform);

                    building.rocksParent.GetChild(building.rockCount).DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(delayCountForPieces);
                    delayCountForPieces += 0.05f;
                    building.rockCount += 1;
                    building.CheckPropCount();

                    if (building.GetInstanceID() == GameManager.Instance.building1.GetInstanceID())
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b1_RockCount, building.rocksParent, building.rockCount);
                    }
                    else
                    {
                        GameManager.Instance.uiManager.RefreshPropCountsByBuilding(GameManager.Instance.uiManager.b2_RockCount, building.rocksParent, building.rockCount);
                    }

                    prop.transform.parent = GameManager.Instance.rockPool.transform;
                }
            }
            PropAnimations(building.transform);
            StopCoroutine(nameof(RearrangeBackPack));
            StartCoroutine(nameof(RearrangeBackPack));
            //RearrangeBackpack();
        }
    }
    void PropAnimations(Transform building)
    {
        float delayCount = 0;
        for (int i = 0; i<usedGolds.Count; i++)
        {
            GameObject gold;
            gold = usedGolds[i].gameObject;
            goldsAtBackPack.Remove(gold.transform);
            gold.transform.DOMove(building.transform.position, 0.2f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() => GameManager.Instance.goldPool.ReturnGoldPrefab(gold.gameObject));
            delayCount += 0.02f;
        }
        usedGolds.Clear();
        for (int i = 0; i < usedWoods.Count; i++)
        {
            GameObject wood;
            wood = usedWoods[i].gameObject;
            woodsAtBackPack.Remove(wood.transform);
            wood.transform.DOMove(building.transform.position, 0.2f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() => GameManager.Instance.woodPool.ReturnWoodPrefab(wood.gameObject));
            delayCount += 0.02f;
        }
        usedWoods.Clear();
        for (int i = 0; i < usedRocks.Count; i++)
        {
            GameObject rock;
            rock = usedRocks[i].gameObject;
            rocksAtBackPack.Remove(rock.transform);
            rock.transform.DOMove(building.transform.position, 0.22f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() => GameManager.Instance.rockPool.ReturnRockPrefab(rock.gameObject));
            delayCount += 0.02f;
        }
        usedRocks.Clear();

    }
    IEnumerator RearrangeBackPack()
    {
        yield return new WaitForSeconds(0.2f);
        float delayCount = 0.1f;
        float nextSlotPosY = 0;
        for (int i = 0; i < propsAtBackpack.Count; i++)
        {
            propsAtBackpack[i].transform.DOLocalMove(new Vector3(0, nextSlotPosY, 0), 0.12f).SetDelay(delayCount);
            nextSlotPosY += 0.28f;
            delayCount += 0.02f;
        }
        GameManager.Instance.nextSlotPosY = nextSlotPosY;
    }
    void RefundProps()
    {
        float delayCount = 0;
        for (int i = propsAtBackpack.Count-1; i >= 0; i--)
        {
            GameObject prop;
            prop = propsAtBackpack[i].gameObject;
            delayCount += 0.05f;
            if (prop.layer == 7)
            {

                prop.transform.DOMove(refundPoint.transform.position, 0.15f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() =>
                {
                    prop.transform.parent = GameManager.Instance.woodPool.transform;
                    GameManager.Instance.woodPool.ReturnWoodPrefab(prop);
                    propsAtBackpack.Remove(prop.transform);

                });
            }
            else if(prop.layer == 8)
            {
                prop.transform.DOMove(refundPoint.transform.position, 0.15f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() =>
                {
                    prop.transform.parent = GameManager.Instance.goldPool.transform;
                    GameManager.Instance.goldPool.ReturnGoldPrefab(prop);
                    propsAtBackpack.Remove(prop.transform);

                });
            }
            else if (prop.layer == 9)
            {
                prop.transform.DOMove(refundPoint.transform.position, 0.15f).SetDelay(delayCount).SetEase(Ease.Unset).OnComplete(() =>
                {
                    prop.transform.parent = GameManager.Instance.rockPool.transform;
                    GameManager.Instance.rockPool.ReturnRockPrefab(prop);
                    propsAtBackpack.Remove(prop.transform);

                });
            }



        }
        propsAtBackpack.Clear();
        woodsAtBackPack.Clear();
        goldsAtBackPack.Clear();
        rocksAtBackPack.Clear();
        GameManager.Instance.nextSlotPosY = 0;

    }
}
