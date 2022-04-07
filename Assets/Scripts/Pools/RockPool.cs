using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    Queue<GameObject> rockQ = new Queue<GameObject>();
    public GameObject rockPrefab;
    private static readonly int rockPoolCount = 30;

    private void Start()
    {
        for (int i = 0; i < rockPoolCount; i++)
        {
            GameObject rock = Instantiate(rockPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            rockQ.Enqueue(rock);
            rock.SetActive(false);
        }
    }

    public GameObject GetRockPrefab()
    {
        if (rockQ.Count > 0)
        {
            GameObject rock = rockQ.Dequeue();
            rock.SetActive(true);
            return rock;
        }
        else
        {
            Debug.Log("Rock");
            GameObject rock = Instantiate(rockPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            return rock;
        }
    }
    public void ReturnRockPrefab(GameObject rock)
    {
        rock.SetActive(false);
        rockQ.Enqueue(rock);
    }
}
