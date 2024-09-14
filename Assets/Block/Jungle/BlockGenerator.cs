using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public List<GameObject> blocks = new List<GameObject>(); // ������ ���� ������
    public GameObject caveEnterBlock; // ���� ��� ����� � ������
    public GameObject caveExitBlock; // ���� ��� ������ �� ������
    public float caveBlockChance = 0.1f; // ����������� ��������� �������� ������ (10%)

    private List<GameObject> spawnList = new List<GameObject>(); // ������ ������ ��� ������
    private int currentIndex = 0; // ������� ������ ��� ������

    public void SpawnBlock()
    {
        // ���������, �� ����� �� �� �� ������� ������ ������
        if (currentIndex < spawnList.Count)
        {
            GameObject blockPrefab = spawnList[currentIndex]; // �������� ������� ���� �� ������
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            currentIndex++; // ����������� ������ ��� ���������� ������
        }
        else
        {
            Debug.Log("All blocks have been spawned."); // ������� ���������, ���� ��� ����� ��� ���� ��������
        }
    }

    private void GenerateSpawnList()
    {
        spawnList.Clear(); // ������� ������ ����� ����������

        // ���������� ������ ������ ��� ������
        for (int i = 0; i < 20; i++)
        {
            if (Random.value <= caveBlockChance) // ��������� ����������� ��������� ��������� �����
            {
                spawnList.Add(caveEnterBlock);
                spawnList.Add(caveExitBlock);
            }
            else
            {
                spawnList.Add(blocks[Random.Range(0, blocks.Count)]);
            }
        }

        currentIndex = 0; // ���������� ������
    }

    // �������� GenerateSpawnList ��� ������, ����� ����������� ������ ������ ��� ������
    private void Start()
    {
        GenerateSpawnList();
    }
}