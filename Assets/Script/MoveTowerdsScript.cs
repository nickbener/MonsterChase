using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowerdsScript : MonoBehaviour
{
    public string monsterTag = "Monster"; // Тег объектов Monster
    public float moveSpeed = 2.0f; // Скорость перемещения монстра
    private Transform monsterToMove; // Ссылка на монстра, который будет перемещаться

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(monsterTag))
        {
            monsterToMove = collision.transform; // Запомнить монстра, который будет перемещаться
        }
    }

    private void Update()
    {
        if (monsterToMove != null)
        {
            // Используйте метод MoveTowards для постепенного перемещения монстра к игроку
            monsterToMove.position = Vector3.MoveTowards(monsterToMove.position, transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(monsterTag))
        {
            monsterToMove = null; // Сбросить ссылку на монстра, когда он покидает зону
        }
    }
}