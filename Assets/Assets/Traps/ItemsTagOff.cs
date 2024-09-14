using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTagOff : MonoBehaviour
{
    private GameObject tagOffObj;
    
    void Start()
    {
        tagOffObj = GameObject.Find("ColliderToChangeTags");
    }

    
    void Update()
    {
        if (transform.position.x < tagOffObj.transform.position.x)
        {
            gameObject.tag = "NullOfAll";
        }
    }

    void CdTag()
    {
    }
}
