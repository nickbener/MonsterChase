using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapayaFruitsLinePosition : MonoBehaviour
{
    private GameObject[] items;
    private GameObject[] itemNulls;
    private GameObject closest;

    private void Start()
    {
        Invoke("SpawnCD", 0.3f);
        
    }

    void SpawnCD()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        int randomIndexOfItem = Random.Range(0, items.Length);
        
        List<int> selectedIndices = new List<int>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name != "Item (4)")
            {
                selectedIndices.Add(i);
            }
        }
        
        int randomIndex = Random.Range(0, selectedIndices.Count);
        int randomObjectIndex = selectedIndices[randomIndex];
        GameObject randomObject = items[randomObjectIndex];
        transform.position = new Vector2(randomObject.transform.position.x, randomObject.transform.position.y - 0.22f);
        
        Transform parentTransform = FindClosestItem().transform.parent;

        if (parentTransform != null)
        {
            foreach (Transform child in parentTransform)
            {
                child.gameObject.tag = "NullOfAll";
            }
        }
    }
    
    GameObject FindClosestItem()
    {
        float distance = Mathf.Infinity;
        foreach (GameObject go in items)
        {
            Vector2 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
