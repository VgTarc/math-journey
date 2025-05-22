using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class RectTransformSizeLogger : MonoBehaviour
{
    void Start()
    {
        // ����� Unity �Ѵ layout ���稡�͹
        StartCoroutine(LogRectSizeAfterFrame());
    }

    IEnumerator LogRectSizeAfterFrame()
    {
        yield return new WaitForEndOfFrame();

        RectTransform rt = GetComponent<RectTransform>();
        float width = rt.rect.width;
        float height = rt.rect.height;

        Debug.Log($"[RectTransformSizeLogger] UI Size = {width} x {height}");

        // ����� Image ������ҡ�٢�Ҵ texture ��ԧ�ͧ sprite ����
        Image img = GetComponent<Image>();
        if (img != null && img.sprite != null)
        {
            Texture2D tex = img.sprite.texture;
            Debug.Log($"[RectTransformSizeLogger] Sprite texture size = {tex.width} x {tex.height}");
        }
    }
}
