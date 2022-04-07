using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject nextLevelBtn;
    public Image fadeImage;

    [Header("Building 1 Prop Count Texts")]
    public Text b1_GoldCount;
    public Text b1_WoodCount;
    public Text b1_RockCount;

    [Header("Building 2 Prop Count Texts")]
    public Text b2_GoldCount;
    public Text b2_WoodCount;
    public Text b2_RockCount;

    [Header("Soldier 1 Prop Count Texts")]
    public Text s1_GoldCount;
    public Text s1_RockCount;

    [Header("Soldier 2 Prop Count Texts")]
    public Text s2_GoldCount;
    public Text s2_RockCount;

    [Header("Solider Count Texts")]
    public Text bowerCount;
    public Text spearCount;

    [Header("Building Canvases")]
    public GameObject b1_PropCanvas;
    public GameObject b2_PropCanvas;
    public GameObject b1_SoldierCanvas;
    public GameObject b2_SoldierCanvas;
    void Start()
    {
        SetUpMineCounts();
    }

    public void EndOfLevelAnimations()
    {
        nextLevelBtn.SetActive(true);
        nextLevelBtn.transform.DOScale(Vector3.one, 0.3f).SetDelay(2f);
    }
    public void FadeInOutScreen()
    {
        fadeImage.DOFade(1, 0.2f);
        fadeImage.DOFade(0, 0.3f).SetDelay(0.3f);
    }
    public void SetUpMineCounts()
    {
        b1_PropCanvas.SetActive(true);
        b1_SoldierCanvas.SetActive(false);

        b2_PropCanvas.SetActive(true);
        b2_SoldierCanvas.SetActive(false);

        b1_GoldCount.text = GameManager.Instance.building1.goldsParent.childCount.ToString();
        b1_WoodCount.text = GameManager.Instance.building1.woodsParent.childCount.ToString();
        b1_RockCount.text = GameManager.Instance.building1.rocksParent.childCount.ToString();

        b2_GoldCount.text = GameManager.Instance.building2.goldsParent.childCount.ToString();
        b2_WoodCount.text = GameManager.Instance.building2.woodsParent.childCount.ToString();
        b2_RockCount.text = GameManager.Instance.building2.rocksParent.childCount.ToString();

        SetUpSoldierPropCounts();
    }
    public void RefreshPropCountsByBuilding(Text propText, Transform propParent, int propCount)
    {
        propText.text = (propParent.childCount - propCount).ToString();

    }
    public void SetUpSoldierPropCounts()
    {
        s1_GoldCount.text = GameManager.Instance.building1.buildingData.GoldCountForSolider.ToString();
        s1_RockCount.text = GameManager.Instance.building1.buildingData.RockCountForSolider.ToString();

        s2_GoldCount.text = GameManager.Instance.building2.buildingData.GoldCountForSolider.ToString();
        s2_RockCount.text = GameManager.Instance.building2.buildingData.RockCountForSolider.ToString();

        bowerCount.text = GameManager.Instance.building1.buildingData.SoldierCount.ToString();
        spearCount.text = GameManager.Instance.building2.buildingData.SoldierCount.ToString();
    }
}
