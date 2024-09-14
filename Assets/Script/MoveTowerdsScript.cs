using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowerdsScript : MonoBehaviour
{
    public string monsterTag = "Monster"; // ��� �������� Monster
    public float moveSpeed = 2.0f; // �������� ����������� �������
    private Transform monsterToMove; // ������ �� �������, ������� ����� ������������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(monsterTag))
        {
            monsterToMove = collision.transform; // ��������� �������, ������� ����� ������������
        }
    }

    private void Update()
    {
        if (monsterToMove != null)
        {
            // ����������� ����� MoveTowards ��� ������������ ����������� ������� � ������
            monsterToMove.position = Vector3.MoveTowards(monsterToMove.position, transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(monsterTag))
        {
            monsterToMove = null; // �������� ������ �� �������, ����� �� �������� ����
        }
    }
}