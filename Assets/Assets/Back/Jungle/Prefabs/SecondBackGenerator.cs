using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBackGenerator : MonoBehaviour
{
    public GameObject jungleBack;
    public GameObject desertBack;

    public void SpawnBack()
    {
        StartCoroutine(SpawnGround());
    }

    IEnumerator SpawnGround()
    {
        TransformForVector.Location currentLocation = TransformForVector.GetCurrentLocation();
        if (currentLocation == TransformForVector.Location.jungle)
        {
            Instantiate(jungleBack, transform.position, Quaternion.identity);
        }
        else if (currentLocation == TransformForVector.Location.desert)
        {
            Instantiate(desertBack, transform.position, Quaternion.identity);
        }
        yield return null;
    }
}
