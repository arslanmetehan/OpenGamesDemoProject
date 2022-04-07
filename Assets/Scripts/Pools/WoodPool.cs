using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPool : MonoBehaviour
{
    Queue<GameObject> woodQ = new Queue<GameObject>();
    public GameObject woodPrefab;
    private static readonly int woodPoolCount = 30;

    private void Start()
    {
        for (int i = 0; i < woodPoolCount; i++)
        {
            GameObject wood = Instantiate(woodPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            woodQ.Enqueue(wood);
            wood.SetActive(false);
        }

    }

    public GameObject GetWoodPrefab()
    {
        if (woodQ.Count > 0)
        {
            GameObject wood = woodQ.Dequeue();
            wood.SetActive(true);
            return wood;
        }
        else
        {
            Debug.Log("Wood");
            GameObject wood = Instantiate(woodPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            return wood;
        }
    }
    public void ReturnWoodPrefab(GameObject wood)
    {
        wood.SetActive(false);
        woodQ.Enqueue(wood);
    }
}
