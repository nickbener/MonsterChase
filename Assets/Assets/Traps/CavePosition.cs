using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavePosition : MonoBehaviour
{
    private GameObject[] platformFind;
    private GameObject[] groundFind;
    private int platformIndex;
    private int groundIndex;
    private int random;
    private int randomPosXForPlatform;
    private int randomPosXForGround;

    void Start()
    {
        platformFind = GameObject.FindGameObjectsWithTag("Platform");
        platformIndex = Random.Range(0, platformFind.Length);
        randomPosXForPlatform = Random.Range(-1, 3);
    }
    private void Update()
    {
        Vector2 nerestVector = new Vector2(platformFind[platformIndex].transform.position.x + randomPosXForPlatform, platformFind[platformIndex].transform.position.y + 1f);
        transform.position = nerestVector;
        platformFind[platformIndex].gameObject.tag = "Null";
    }

}
