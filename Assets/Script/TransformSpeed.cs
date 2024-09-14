using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpeed : MonoBehaviour
{
    [HideInInspector] public float baseSpeed = 6.0f;
    public float speed;
    public Vector2 vec;
    public bool isDestroyingToTrap = false;
    public bool changeTagsBollion = false;
    public bool ifThisIsBack = false;
    private GameObject obj;
    private GameObject objTrap;
    private GameObject objChangeTag;
    private PlayerController playerController;


    void Start()
    {
        baseSpeed = 6.0f;
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.UpdateTransformSpeedArray();
        if (isDestroyingToTrap == false)
        {
            obj = GameObject.Find("DestroyPoint");
        }

        if (isDestroyingToTrap)
        {
            objTrap = GameObject.Find("DestroyPointForTraps");
        }

        if (changeTagsBollion)
        {
            objChangeTag = GameObject.Find("ColliderToChangeTags");
        }

    }

    void FixedUpdate()
    {
        transform.Translate(vec * speed * Time.deltaTime);

        if (isDestroyingToTrap == false)
        {
            if (transform.position.x < obj.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
        else if (isDestroyingToTrap)
        {
            if (transform.position.x < objTrap.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
        if (changeTagsBollion)
        {
            if (transform.position.x < objChangeTag.transform.position.x)
            {
                //this.gameObject.tag = "NullForAll";
            }
        }

    }
}
