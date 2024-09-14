using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallTrapPosition : MonoBehaviour
{
    public string itemTag = "Item"; // Тег для поиска Item-ов
    public float detectionRadius; // Радиус обнаружения Item-ов

    private void Start()
    {
        MoveStoneToRandomValidItem();
    }

    private void MoveStoneToRandomValidItem()
    {
        // Найдите все объекты с тегом "Item"
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag(itemTag);

        // Отфильтруйте Item-ы, которые находятся ниже -2 по оси Y
        List<GameObject> validItems = new List<GameObject>();
        foreach (var item in itemObjects)
        {
            if (item.transform.position.y < -2)
            {
                validItems.Add(item);
            }
        }

        if (validItems.Count > 0)
        {
            // Выберите случайный Item из отфильтрованных
            GameObject randomItem = validItems[Random.Range(0, validItems.Count)];

            // Переместите камень к позиции случайного Item-а (моментально)
            Vector2 vectorItem = new Vector2(randomItem.transform.position.x, randomItem.transform.position.y - 0.19f);
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