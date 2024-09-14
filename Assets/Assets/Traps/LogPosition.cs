//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPosition : MonoBehaviour
{
    private string itemTag = "Item"; // ��� ��� ������ Item-��
    private string brevnoTag = "Stone"; // ��� ��� ������ �����
    public float posY;
    public float detectionRadius = 2f; // ������ ����������� Item-��
    public float stoneDetectionRadius = 3f; // ������ ����������� �����
    public float maxHeight = -2f; // ������������ ������, �� ������� ������ ����� ���� ���������

    private void Start()
    {
        MoveStoneToRandomItem();
    }

    private void MoveStoneToRandomItem()
    {
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag(itemTag);

        // ���������� Item-�� � ������ �������
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
            // �������� ��������� Item �� ���������������
            GameObject randomItem = validItems[Random.Range(0, validItems.Count)];

            Vector2 vectorItem = new Vector2(randomItem.transform.position.x, randomItem.transform.position.y + posY);

            // ����������� ������ � ������� ���������� Item-� (�����������)
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