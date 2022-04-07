using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : MonoBehaviour
{
    Queue<GameObject> goldQ = new Queue<GameObject>();
    public GameObject goldPrefab;
    private static readonly int goldPoolCount = 30;

    private void Start()
    {
        for (int i = 0; i < goldPoolCount; i++)
        {
            GameObject gold = Instantiate(goldPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            goldQ.Enqueue(gold);
            gold.SetActive(false);
        }
    }

    public GameObject GetGoldPrefab()
    {
        if (goldQ.Count > 0)
        {
            GameObject gold = goldQ.Dequeue();
            gold.SetActive(true);
            return gold;
        }
        else
        {
            Debug.Log("Gold");
            GameObject gold = Instantiate(goldPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            return gold;
        }
    }
    public void ReturnGoldPrefab(GameObject gold)
    {
        gold.SetActive(false);
        goldQ.Enqueue(gold);
    }
}
