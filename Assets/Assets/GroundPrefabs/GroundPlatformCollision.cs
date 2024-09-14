using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlatformCollision : MonoBehaviour
{
    public string name;
    public string tag;
    private GameObject generator;
    private GroundGenerator groundGeneratorScript;

    private void Start()
    {
        generator = GameObject.Find(name);
        groundGeneratorScript = generator.GetComponent<GroundGenerator>();
        
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            if (!this.gameObject.scene.isLoaded) return;
            groundGeneratorScript.SpawnGroundPlatform();
        }
    }

}