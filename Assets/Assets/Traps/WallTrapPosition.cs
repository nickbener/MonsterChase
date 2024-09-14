using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallTrapPosition : MonoBehaviour
{
    public string itemTag = "Item"; // ��� ��� ������ Item-��
    public float detectionRadius; // ������ ����������� Item-��

    private void Start()
    {
        MoveStoneToRandomValidItem();
    }

    private void MoveStoneToRandomValidItem()
    {
        // ������� ��� ������� � ����� "Item"
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag(itemTag);

        // ������������ Item-�, ������� ��������� ���� -2 �� ��� Y
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
            // �������� ��������� Item �� ���������������
            GameObject randomItem = validItems[Random.Range(0, validItems.Count)];

            // ����������� ������ � ������� ���������� Item-� (�����������)
            Vector2 vectorItem = new Vector2(randomItem.transform.position.x, randomItem.transform.position.y - 0.19f);
            transform.position = vectorItem;

            // �������� ���� Item-�� � ������� �����������
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