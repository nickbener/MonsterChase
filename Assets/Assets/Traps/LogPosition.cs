//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPosition : MonoBehaviour
{
    private string itemTag = "Item"; // Тег для поиска Item-ов
    private string brevnoTag = "Stone"; // Тег для поиска брёвен
    public float posY;
    public float detectionRadius = 2f; // Радиус обнаружения Item-ов
    public float stoneDetectionRadius = 3f; // Радиус обнаружения брёвен
    public float maxHeight = -2f; // Максимальная высота, на которой камень может быть перемещен

    private void Start()
    {
        MoveStoneToRandomItem();
    }

    private void MoveStoneToRandomItem()
    {
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag(itemTag);

        // Фильтрация Item-ов с учетом условий
        List<GameObject> validItems = new List<GameObject>();

        foreach (var item in itemObjects)
        {
            if (item.transform.position.y > maxHeight)
            {
                Collider2D[] brevnoColliders = Physics2D.OverlapCircleAll(item.transform.position, stoneDetectionRadius);
                bool hasBrevno = false;

                foreach (var brevnoCollider in brevnoColliders)
                {
                    if (brevnoCollider.CompareTag(brevnoTag))
                    {
                        hasBrevno = true;
                        break;
                    }
                }

                if (!hasBrevno)
                {
                    validItems.Add(item);
                }
            }
            else
            {
                validItems.Add(item);
            }
        }

        if (validItems.Count > 0)
        {
            // Выберите случайный Item из отфильтрованных
            GameObject randomItem = validItems[Random.Range(0, validItems.Count)];

            Vector2 vectorItem = new Vector2(randomItem.transform.position.x, randomItem.transform.position.y + posY);

            // Переместите камень к позиции случайного Item-а (моментально)
            transform.position = vectorItem;

            // Измените теги Item-ов в радиусе обнаружения
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(itemTag))
                {
                    collider.tag = "NullOfAll";
                }
            }
        }
    }
}