using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockExitCollision : MonoBehaviour
{

    private GameObject blockGenerator;

    void Start()
    {
        blockGenerator = GameObject.FindGameObjectWithTag("JungleBlockGenerator");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JungleBlockGenerator"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            blockGenerator.GetComponent<BlockGenerator>().SpawnBlock();
        }
    }
}