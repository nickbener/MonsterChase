using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBlockExitCollision : MonoBehaviour
{
    private GameObject blockGenerator;

    void Start()
    {
        blockGenerator = GameObject.FindGameObjectWithTag("CaveBlockGenerator");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CaveBlockGenerator"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            blockGenerator.GetComponent<CaveBlockGenerator>().SpawnCaveBlock();
        }
    }
}