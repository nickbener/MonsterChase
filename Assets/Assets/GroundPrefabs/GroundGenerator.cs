using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [Header("Jungle")]
    public GameObject jungleGround;
    public GameObject[] abyssJungle;

    //[Header("Desert")]
    //public GameObject desertGround;
    //public GameObject[] abyssDesert;

    public PlayerController playerController;

    private int chance;
    public float startFirstVector;
    //public bool jungleMustLoaded;

    void Start()
    {
        //jungleMustLoaded = true;
    }

    private void Update()
    {
    }

    public void SpawnGroundPlatform()
    {
        StartCoroutine(SpawnGround());
    }

    IEnumerator SpawnGround()
    {
        chance = Random.Range(0, 100);
        //TransformForVector.Location currentLocation = TransformForVector.GetCurrentLocation();

        //if (currentLocation == TransformForVector.Location.jungle)
        //{
            
        //}
        if (chance < 50)
        {
            Instantiate(jungleGround, transform.position, Quaternion.identity);
            playerController.UpdateTransformSpeedArray();
        }
        else if (chance >= 50)
        {
            Instantiate(abyssJungle[Random.Range(0, 2)], transform.position, Quaternion.identity);
            playerController.UpdateTransformSpeedArray();
        }
        //else if (currentLocation == TransformForVector.Location.desert)
        //{
        //    if (chance < 50)
        //    {
        //        Instantiate(desertGround, transform.position, Quaternion.identity);
        //        playerController.UpdateTransformSpeedArray();
        //    }
        //    else if (chance >= 50)
        //    {
        //        Instantiate(abyssDesert[Random.Range(0, 2)], transform.position, Quaternion.identity);
        //        playerController.UpdateTransformSpeedArray();
        //    }
        //}

        yield return null;
    }
}
