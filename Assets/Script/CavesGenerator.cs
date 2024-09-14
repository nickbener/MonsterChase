using System.Collections.Generic;
using UnityEngine;
//using Random = Unity.Mathematics.Random;

public class CavesGenerator : MonoBehaviour
{
    public GameObject cave;
    public int minCountCaves;
    public int maxCountCaves;
    
    private int allCountCaves;
    private GameObject vectorObj;
    private Dictionary<int, float> mapVector;

    private List<int> randomNumbers = new List<int>();

    void Start()
    {
        vectorObj = GameObject.Find("Vector");
        mapVector = new Dictionary<int, float>();
        allCountCaves = Random.Range(minCountCaves, maxCountCaves);
        
        for (int i = 0; i < 500; i++)
        {
            mapVector.Add(i, transform.position.y);
        }

        for (int i = 0; i < allCountCaves; i++)
        {
            int randomNum = Random.Range(0, 500);
            randomNumbers.Add(randomNum);
            //Debug.Log(randomNum);
        }
    }

    void Update()
    {
        float floatX = vectorObj.transform.position.x;
        int vectorRoundX = Mathf.RoundToInt(floatX);
        
        foreach (int randomNumber in randomNumbers)
        {
            // ѕровер€ем, совпадает ли позици€ vectorObj с одним из случайных чисел
            if (vectorRoundX == randomNumber)
            {
                float coordinate = mapVector[randomNumber];
                Vector2 whereToSpawn = new Vector2(transform.position.x, coordinate);
                Instantiate(cave, whereToSpawn, Quaternion.identity);
                randomNumbers.Remove(randomNumber); // ”дал€ем совпавшее число из списка
                break; // ѕрерываем цикл, чтобы не спавнить дополнительные префабы
            }
        }
    }
}