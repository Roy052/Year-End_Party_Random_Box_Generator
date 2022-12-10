using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeManager : MonoBehaviour
{
    public static IEnumerator FadeIn(SpriteRenderer spriteRenderer, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = spriteRenderer.color;
        tempColor.a = 0;
        spriteRenderer.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a += Time.deltaTime / time;
            spriteRenderer.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeOut(SpriteRenderer spriteRenderer, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = spriteRenderer.color;
        tempColor.a = 1;
        spriteRenderer.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a -= Time.deltaTime / time;
            spriteRenderer.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeIn(Image image, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = image.color;
        tempColor.a = 0;
        image.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a += Time.deltaTime / time;
            image.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeOut(Image image, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = image.color;
        tempColor.a = 1;
        image.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a -= Time.deltaTime / time;
            image.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeIn(TextMeshProUGUI tmpText, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = tmpText.color;
        tempColor.a = 0;
        tmpText.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a += Time.deltaTime / time;
            tmpText.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeOut(TextMeshProUGUI tmpText, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = tmpText.color;
        tempColor.a = 1;
        tmpText.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a -= Time.deltaTime / time;
            tmpText.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
