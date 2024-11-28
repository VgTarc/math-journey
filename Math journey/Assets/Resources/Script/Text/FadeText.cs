using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    
    private TextMeshProUGUI fadeText;
    private float alphaValue;
    private float fadeAwayPerSec;

    [Header("Fade Time")]
    public float fadeOutTime;
    public float fadeInTime;
    public float displayTime;

    private void Start()
    {
        fadeText = GetComponent<TextMeshProUGUI>();
        fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 0);
        fadeAwayPerSec = 1 / fadeOutTime;
        StartCoroutine(FadeInOut());
    }

    public IEnumerator FadeInOut()
    {
        yield return Fade(fadeInTime, 0f, 1f); // From 0 (transparent) to 1 (opaque)

        yield return new WaitForSeconds(displayTime); // Wait for display time at full opacity

        yield return Fade(fadeOutTime, 1f, 0f); // From 1 (opaque) to 0 (transparent)
    }

    private IEnumerator Fade(float duration, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        float currentAlpha = startAlpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, currentAlpha);
            yield return null; 
        }

      
        fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, endAlpha);
    }
}