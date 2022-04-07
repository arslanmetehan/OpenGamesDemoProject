using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingManager : MonoBehaviour
{
    public void CompleteBuilding(Transform building)
    {
        building.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetDelay(0.5f).OnComplete(() => building.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
        if(building.GetComponent<Building>().GetInstanceID() == GameManager.Instance.building1.GetComponent<Building>().GetInstanceID())
        {
            GameManager.Instance.uiManager.b1_PropCanvas.SetActive(false);
            GameManager.Instance.uiManager.b1_SoldierCanvas.SetActive(true);
        }
        else
        {
            GameManager.Instance.uiManager.b2_PropCanvas.SetActive(false);
            GameManager.Instance.uiManager.b2_SoldierCanvas.SetActive(true);
        }
    }
    public void GetBuildFromLevel(LevelDatSO nextLevelData)
    {
        Destroy(GameManager.Instance.building1.gameObject);
        Destroy(GameManager.Instance.building2.gameObject);

        for(int i = 0; i < GameManager.Instance.soldierParent.childCount; i++)
        {
            Destroy(GameManager.Instance.soldierParent.GetChild(i).gameObject);
        }


        GameObject building1 = Instantiate(nextLevelData.building1, Vector3.zero, Quaternion.identity, GameManager.Instance.building1Parent);
        GameManager.Instance.building1 = building1.GetComponent<Building>();
        building1.transform.localPosition = Vector3.zero;
        building1.transform.rotation = Quaternion.Euler(Vector3.zero);
        building1.transform.DOScale(Vector3.one, 0.5f).SetDelay(1f);

        GameObject building2 =  Instantiate(nextLevelData.building2, Vector3.zero, Quaternion.identity, GameManager.Instance.building2Parent);
        GameManager.Instance.building2 = building2.GetComponent<Building>();
        building2.transform.localPosition = Vector3.zero;
        building2.transform.rotation = Quaternion.Euler(Vector3.zero);
        building2.transform.DOScale(Vector3.one, 0.5f).SetDelay(1f);

        GameManager.Instance.building1Parent.GetComponent<BoxCollider>().enabled = true;
        GameManager.Instance.building2Parent.GetComponent<BoxCollider>().enabled = true;

    }
}
