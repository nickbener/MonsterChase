using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBackCollision : MonoBehaviour
{
    private GameObject firstbackGenerator;
    private FirstBackGenerator backGeneratorScript;

    private void Start()
    {
        firstbackGenerator = GameObject.Find("SpawnPosBack1");
        backGeneratorScript = firstbackGenerator.GetComponent<FirstBackGenerator>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FirstBackCollision"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            backGeneratorScript.SpawnBack();
        }
    }
}
