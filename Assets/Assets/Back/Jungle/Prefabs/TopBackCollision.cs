using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBackCollision : MonoBehaviour
{
    private GameObject topbackGenerator;
    private TopBackGenerator backGeneratorScript;

    private void Start()
    {
        topbackGenerator = GameObject.Find("SpawnPosBackTop");
        backGeneratorScript = topbackGenerator.GetComponent<TopBackGenerator>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TopBackCollision"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            backGeneratorScript.SpawnBack();
        }
    }
}
