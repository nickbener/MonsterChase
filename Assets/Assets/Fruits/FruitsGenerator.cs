using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsGenerator : MonoBehaviour
{
    public List<GameObject> fruits = new List<GameObject>();

    private int a;
    private Dictionary<int, float> mapVector;
    private GameObject vectorObj;
    private int vectorRoundX;
    private GameObject parentObj;


    void Start()
    {
        vectorObj = GameObject.Find("Vector");
        parentObj = GameObject.Find("Level");
        mapVector = new Dictionary<int, float>();

        int interval = Random.Range(10, 50);
        for (int i = interval; i < 500; i += interval)
        {
            mapVector.Add(i, transform.position.y);
        }
    }

    void Update()
    {
        float floatX = vectorObj.transform.position.x;
        vectorRoundX = Mathf.RoundToInt(floatX);

        if (mapVector.ContainsKey(vectorRoundX))
        {
            var newGround = Instantiate(fruits[Random.Range(0, fruits.Count)], transform.position, Quaternion.identity);
            newGround.transform.parent = parentObj.transform;
            mapVector.Remove(vectorRoundX);
        }
    }
}
