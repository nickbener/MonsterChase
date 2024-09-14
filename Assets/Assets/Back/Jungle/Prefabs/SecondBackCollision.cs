using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBackCollision : MonoBehaviour
{
    private GameObject secondbackGenerator;
    private SecondBackGenerator backGeneratorScript;

    private void Start()
    {
        secondbackGenerator = GameObject.Find("SpawnPosBack2");
        backGeneratorScript = secondbackGenerator.GetComponent<SecondBackGenerator>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SecondBackCollision"))
        {
            if (!this.gameObject.scene.isLoaded) return;
            backGeneratorScript.SpawnBack();
        }
    }
}
