using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    [HideInInspector] public List<GameObject> allPlatforms = new List<GameObject>();
    [HideInInspector] public int[] values;
    [HideInInspector] public List<GameObject> breakePlatforms = new List<GameObject>();
    [HideInInspector] public int[] breakeValues;
    [HideInInspector] public GameObject hightPlatform;
    [HideInInspector] public int valueOfHightPlatform;
    [HideInInspector] public int minCountPlatform;
    [HideInInspector] public int maxCountPlatform = 70;

    private float totalPlatformsvalue;
    private float restPlatformsValue;
    private int a;
    private int interval;
    private int vectorRoundX;
    private Dictionary<int, float> mapVector;
    private GameObject vectorObj;
    private GameObject parentObj;
    private int randomNormalindex;
    private int randomBreakeindex;


    void Start()
    {
        vectorObj = GameObject.Find("Vector");
        mapVector = new Dictionary<int, float>();
        parentObj = GameObject.Find("Level");

        totalPlatformsvalue = Random.Range(minCountPlatform, maxCountPlatform);
        Debug.Log(totalPlatformsvalue);
        float percent = maxCountPlatform / totalPlatformsvalue;
        interval = Mathf.RoundToInt(20 * percent);

        for (int i = 0; i < 500; i += interval)
        {
            mapVector.Add(i, transform.position.y);
        }

        restPlatformsValue = totalPlatformsvalue;
    }

    private void Update()
    {
        
        float floatX = vectorObj.transform.position.x;
        vectorRoundX = Mathf.RoundToInt(floatX);

        if (mapVector.ContainsKey(vectorRoundX))
        {
            int percentRand = Random.Range(0, 100);
            if(percentRand <= 80)
            {

                // Вероятность появления обычных платформ = 75
                int percentRandBreake = Random.Range(0, 100);
                if (percentRandBreake <= 65)
                {
                    randomNormalindex = Random.Range(0, allPlatforms.Count);
                    float cordinate = mapVector[vectorRoundX];
                    Vector2 whereToSpawn = new Vector2(transform.position.x, cordinate);

                    var newObj = Instantiate(allPlatforms[randomNormalindex], whereToSpawn, Quaternion.identity);
                    newObj.transform.parent = parentObj.transform;
                    restPlatformsValue -= values[randomNormalindex];
                    mapVector.Remove(vectorRoundX);
                }

                // Вероятность появления хрупких платформ = 25
                else if(percentRandBreake > 65)
                {
                    randomBreakeindex = Random.Range(0, breakePlatforms.Count);
                    float cordinate = mapVector[vectorRoundX];
                    Vector2 whereToSpawn = new Vector2(transform.position.x, cordinate);

                    var newObj = Instantiate(breakePlatforms[randomBreakeindex], whereToSpawn, Quaternion.identity);
                    newObj.transform.parent = parentObj.transform;
                    mapVector.Remove(vectorRoundX);
                    restPlatformsValue -= breakeValues[randomBreakeindex];
                    a++;
                    //Debug.Log(a);
                }
                
            }
            else if(percentRand >= 80)
            {
                float cordinate = mapVector[vectorRoundX];
                Vector2 whereToSpawn = new Vector2(transform.position.x, cordinate);

                var newObj = Instantiate(hightPlatform, whereToSpawn, Quaternion.identity);
                newObj.transform.parent = parentObj.transform;
                mapVector.Remove(vectorRoundX);
                restPlatformsValue -= valueOfHightPlatform;
            }
        }
        if(restPlatformsValue < 0)
        {
            Debug.Log("AAA");
        }
    }
    
}
