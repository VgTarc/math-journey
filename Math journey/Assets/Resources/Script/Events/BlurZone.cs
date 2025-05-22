using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurZone : MonoBehaviour
{
    private CanvasGroup blurCanvasGroup;
    public float fadeduration = 1f;
    public float targetAlpha = 0.5f;
    public int Damage = 1;
    private bool Inzone = false;

    private PlayerHealth playerHealth;

    private Coroutine fadeCoroutine;
    private Coroutine takeDamage;


    private void Start()
    {
        blurCanvasGroup = GameObject.Find("DarkCanvas").GetComponent<CanvasGroup>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inzone = true;
            takeDamage = StartCoroutine(TakeDmg(Damage));
            if(fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeBlur(targetAlpha));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inzone = false;
            StopCoroutine(takeDamage);
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeBlur(0f));
        }
    }

    IEnumerator FadeBlur(float targetAlpha)
    {
        float startAlpha = blurCanvasGroup.alpha;
        float time = 0;

        while (time < fadeduration)
        {
            blurCanvasGroup.alpha = Mathf.Lerp(startAlpha,targetAlpha,time / fadeduration);
            time += Time.deltaTime;
            yield return null;
        }
        blurCanvasGroup.alpha = targetAlpha;
    }

    IEnumerator TakeDmg(int Dmg)
    {
        while(Inzone == true)
        {
            playerHealth.TakeDamage(Dmg);
            yield return new WaitForSeconds(5f);
        }
    }

    


}
