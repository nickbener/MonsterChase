using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitPosition : MonoBehaviour
{
    public string itemTag = "Item"; // ��� ��� ������ Item-��
    public float detectionRadius; // ������ ����������� Item-��
    public float posY;

    private void Start()
    {
        MoveStoneToRandomItem();
    }

    private void MoveStoneToRandomItem()
    {
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag(itemTag);

        if (itemObjects.Length > 0)
        {
            // �������� ��������� Item
            GameObject randomItem = itemObjects[Random.Range(0, itemObjects.Length)];

            // ����������� ������ � ������� ���������� Item-� (�����������)
            Vector2 vectorItem = new Vector2(randomItem.transform.position.x, randomItem.transform.position.y + posY);
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
