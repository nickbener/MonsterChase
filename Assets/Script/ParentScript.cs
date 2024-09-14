using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    private GameObject objParent;

    void Start()
    {
        objParent = GameObject.Find("Level");
        transform.parent = objParent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
