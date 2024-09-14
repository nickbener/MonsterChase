using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBackGenerator : MonoBehaviour
{
    public GameObject back;

    public void SpawnBack()
    {
        StartCoroutine(SpawnGround());
    }

    IEnumerator SpawnGround()
    {
        Instantiate(back, transform.position, Quaternion.identity);
        yield return null;
    }
}
