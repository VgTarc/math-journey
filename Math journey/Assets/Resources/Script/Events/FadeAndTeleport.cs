using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportWithFadeAndImageChange : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("Teleport Settings")]
    public GameObject player;
    public Transform teleportTarget;

    [Header("Picture Change Settings")]
    public GameObject pictureParent;          // GameObject ที่มี children เป็น SpriteRenderer
    public Sprite[] newSprites;               // รูปใหม่ 4 รูป

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            isTeleporting = true;
            StartCoroutine(FadeAndTeleport());
        }
    }

    IEnumerator FadeAndTeleport()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Teleport Player
        player.transform.position = teleportTarget.position;

        if(newSprites != null)
        {
                for (int i = 0; i < pictureParent.transform.childCount && i < newSprites.Length; i++)
                {
                    Transform parentChild = pictureParent.transform.GetChild(i);
                    Sprite newSprite = newSprites[i];

                    // เปลี่ยน sprite ของตัวหลัก
                    SpriteRenderer parentRenderer = parentChild.GetComponent<SpriteRenderer>();
                    if (parentRenderer != null)
                    {
                        parentRenderer.sprite = newSprite;
                    }

                    // เปลี่ยน sprite ของลูกภายใน (ที่เป็น SpriteRenderer)
                    foreach (Transform subChild in parentChild)
                    {
                        SpriteRenderer subRenderer = subChild.GetComponent<SpriteRenderer>();
                        if (subRenderer != null)
                        {
                            subRenderer.sprite = newSprite;
                        }
                    }
                }
        }
        


        // รอเล็กน้อยหลัง teleport & เปลี่ยนรูป
        yield return new WaitForSeconds(0.3f);

        // Fade back to normal
        yield return StartCoroutine(Fade(1f, 0f));

        isTeleporting = false;
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
