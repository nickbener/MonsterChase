using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBlockGenerator : MonoBehaviour
{
    public List<GameObject> blocks = new List<GameObject>(); // Список всех блоков
    
    public void SpawnCaveBlock()
    {
        int randomIndex = Random.Range(0, blocks.Count);
        Instantiate(blocks[randomIndex], transform.position, Quaternion.identity);
    }
}