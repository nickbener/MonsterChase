using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    
    public float fadeDuration; // Длительность затемнения и возвращения цвета

    public void WhiteScreen()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        Image image = GetComponent<Image>();

        float timer = 0f;

        // Возвращение прозрачности
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1f, 0f, timer / fadeDuration));
            yield return null;
        }
        gameObject.SetActive(false);
    }
}