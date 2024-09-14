using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public List<GameObject> blocks = new List<GameObject>(); // Список всех блоков
    public GameObject caveEnterBlock; // Блок для входа в пещеру
    public GameObject caveExitBlock; // Блок для выхода из пещеры
    public float caveBlockChance = 0.1f; // Вероятность появления пещерных блоков (10%)

    private List<GameObject> spawnList = new List<GameObject>(); // Список блоков для спавна
    private int currentIndex = 0; // Текущий индекс для спавна

    public void SpawnBlock()
    {
        // Проверяем, не вышли ли мы за пределы списка блоков
        if (currentIndex < spawnList.Count)
        {
            GameObject blockPrefab = spawnList[currentIndex]; // Получаем текущий блок из списка
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            currentIndex++; // Увеличиваем индекс для следующего вызова
        }
        else
        {
            Debug.Log("All blocks have been spawned."); // Выводим сообщение, если все блоки уже были спавнены
        }
    }

    private void GenerateSpawnList()
    {
        spawnList.Clear(); // Очищаем список перед генерацией

        // Генерируем список блоков для спавна
        for (int i = 0; i < 20; i++)
        {
            if (Random.value <= caveBlockChance) // Проверяем вероятность появления пещерного блока
            {
                spawnList.Add(caveEnterBlock);
                spawnList.Add(caveExitBlock);
            }
            else
            {
                spawnList.Add(blocks[Random.Range(0, blocks.Count)]);
            }
        }

        currentIndex = 0; // Сбрасываем индекс
    }

    // Вызываем GenerateSpawnList при старте, чтобы подготовить список блоков для спавна
    private void Start()
    {
        GenerateSpawnList();
    }
}