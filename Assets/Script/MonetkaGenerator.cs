using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonetkaGenerator : MonoBehaviour
{
    [System.Serializable]
    public class CoinTemplate
    {
        public GameObject coinPrefab;
        public int coinValue;
    }


     public List<CoinTemplate> doubleLineTemplates;
     public List<CoinTemplate> lineTemplates;
     public List<CoinTemplate> zigzagTemplates;
    [HideInInspector] public int totalCoinsMinValue = 30;
    [HideInInspector] public int totalCoinsMaxValue = 80;
    [HideInInspector] public float spawnMonetcaCD;

    private List<CoinTemplate> selectedTemplates = new List<CoinTemplate>();

    private void Start()
    {
        StartCoroutine(SpawnCoinsWithDelay());
    }

    private void GenerateTemplates()
    {
        selectedTemplates.Clear();
        int totalCoinsValue = Random.Range(totalCoinsMinValue, totalCoinsMaxValue + 1);
        
        //Debug.Log("Total Coins Value: " + totalCoinsValue);

        int maxAttempts = 100;

        while (totalCoinsValue > 0 && maxAttempts > 0)
        {
            int templateType = Random.Range(0, 3);
            List<CoinTemplate> selectedTemplateList;

            switch (templateType)
            {
                case 0:
                    selectedTemplateList = doubleLineTemplates;
                    break;
                case 1:
                    selectedTemplateList = lineTemplates;
                    break;
                case 2:
                    selectedTemplateList = zigzagTemplates;
                    break;
                default:
                    selectedTemplateList = doubleLineTemplates;
                    break;
            }

            int templateIndex = Random.Range(0, selectedTemplateList.Count);
            CoinTemplate selectedTemplate = selectedTemplateList[templateIndex];
            //Debug.Log("Selected Template Coin Value: " + selectedTemplate.coinValue);

            if (totalCoinsValue - selectedTemplate.coinValue >= 0)
            {
                totalCoinsValue -= selectedTemplate.coinValue;
                selectedTemplates.Add(selectedTemplate);
            }

            maxAttempts--;
        }
    }

    private IEnumerator SpawnCoinsWithDelay()
    {
        GenerateTemplates();

        foreach (CoinTemplate template in selectedTemplates)
        {
            float randomPosY = Random.Range(-2, 3);
            Vector2 whereToSpawn = new Vector2(transform.position.x, randomPosY);
            Instantiate(template.coinPrefab, whereToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(spawnMonetcaCD);
        }
    }

}