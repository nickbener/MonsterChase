using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformForVector : MonoBehaviour
{
    public float speed;
    public GameObject[] desertPrefabs;
    public static Location location;
    public enum Location { jungle, desert }
    public Vector2 vec;
    public static bool jungleMustLoaded;
    public float startFirstVector;
    public float stap;

    private bool desertCanSpawn;

    private void Start()
    {
        desertCanSpawn = false;
    }

    public static Location GetCurrentLocation()
    {
        return location;
    }

    void Update()
    {
        transform.Translate(vec * speed * Time.deltaTime);

        if (transform.position.x > startFirstVector && !desertCanSpawn)
        {
            desertCanSpawn = true;
        }

        if (transform.position.x <= startFirstVector)
        {
            location = Location.jungle;
        }
        else
        {
            location = Location.desert;
        }
    }

    void SpawnDesertPrefabs()
    {
        for (int i = 0; i < desertPrefabs.Length; i++)
        {
            Instantiate(desertPrefabs[i], new Vector2(18.0f, 0), Quaternion.identity);
        }
    }
}
