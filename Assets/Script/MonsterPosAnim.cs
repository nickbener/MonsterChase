using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MonsterPosAnim : MonoBehaviour
{
    public Transform player;
    public Transform monster;
    public float smoothSpeed;
    public PlayerController playerController;
    private Animator monsterAnim;


    private void Update()
    {
        //monsterAnim.SetBool("isBitingBuyOn", playerController.antiBiteIsBuying);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UpMonster") && playerController.antiBiteIsBuying == false)
        {
            // Получите текущее положение монстра и игрока
            Vector3 currentPosition = monster.position;
            Vector3 playerPosition = player.position;

            // Используйте Lerp для плавного движения монстра по оси Y
            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // Установите новую позицию монстра
            transform.position = newPositionMons;
            GameObject monster_obj = GameObject.FindGameObjectWithTag("Monster");
            monsterAnim = monster_obj.GetComponent<Animator>();
            monsterAnim.SetTrigger("isEating");
            monsterAnim.SetInteger("posChooser", 2);
            
            Debug.Log("AAAAA");
        }
        if (collision.CompareTag("MiddleMonster") && playerController.antiBiteIsBuying == false)
        {
            // Получите текущее положение монстра и игрока
            Vector3 currentPosition = monster.position;
            Vector3 playerPosition = player.position;
            // Используйте Lerp для плавного движения монстра по оси Y
            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // Установите новую позицию монстра
            transform.position = newPositionMons;
            GameObject monster_obj = GameObject.FindGameObjectWithTag("Monster");
            monsterAnim = monster_obj.GetComponent<Animator>();
            monsterAnim.SetTrigger("isEating");
            monsterAnim.SetInteger("posChooser", 1);
            Debug.Log("BBBBB");
        }
        if (collision.CompareTag("DownMonster") && playerController.antiBiteIsBuying == false)
        {
            // Получите текущее положение монстра и игрока
            Vector3 currentPosition = monster.position;
            Vector3 playerPosition = player.position;
            // Используйте Lerp для плавного движения монстра по оси Y
            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // Установите новую позицию монстра
            //transform.position = newPositionMons;
            GameObject monster_obj = GameObject.FindGameObjectWithTag("Monster");
            monsterAnim = monster_obj.GetComponent<Animator>();
            monsterAnim.SetTrigger("isEating");
            monsterAnim.SetInteger("posChooser", 0);
            Debug.Log("CCCCC");
        }

        //BITING
        if (collision.CompareTag("DownMonster") && playerController.antiBiteIsBuying == true || collision.CompareTag("DownMonster") && playerController.antiBiteIsBuying == true || collision.CompareTag("UpMonster") && playerController.antiBiteIsBuying == true)
        {
            // Получите текущее положение монстра и игрока
            Vector3 currentPosition = monster.position;
            Vector3 playerPosition = player.position;
            // Используйте Lerp для плавного движения монстра по оси Y
            Vector3 newPositionMons = Vector3.Lerp(currentPosition, playerPosition, smoothSpeed * Time.deltaTime);

            // Установите новую позицию монстра
            //transform.position = newPositionMons;
            GameObject monster_obj = GameObject.FindGameObjectWithTag("Monster");
            monsterAnim = monster_obj.GetComponent<Animator>();
            monsterAnim.SetTrigger("isEating");
            monsterAnim.SetInteger("posChooser", 4);
            Debug.Log("EEEEE");

        }
    }
}
