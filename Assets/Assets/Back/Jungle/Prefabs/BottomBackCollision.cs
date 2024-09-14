using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBackCollision : MonoBehaviour
{
    private GameObject bottombackGenerator;
    private TopBackGenerator backGeneratorScript;

    private void Start()
    {
        bottombackGenerator = GameObject.Find("SpawnPosBackBottom");
        backGeneratorScript = bottombackGenerator.GetComponent<TopBackGenerator>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomBackCollision"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            backGeneratorScript.SpawnBack();
        }
    }
}
