using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMonetkaSpawn : MonoBehaviour
{
    private GameObject monetka;

    void Start()
    {
        float randomNum = Random.Range(0, 3);
        if(randomNum >= 2)
        {
            monetka = GameObject.FindGameObjectWithTag("Coin");
            Vector2 position = new Vector2(transform.position.x, transform.position.y + 0.8f);
            Instantiate(monetka, position, Quaternion.identity);
        }
    }

    
    void Update()
    {
        
    }
}
