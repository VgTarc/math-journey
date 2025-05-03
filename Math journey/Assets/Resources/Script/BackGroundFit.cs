using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class FitBackgroundToCamera : MonoBehaviour
{
    public bool fitWidth = true;
    public bool fitHeight = true;
    public bool alignToCameraBottomLeft = true;

    void Update()
    {
        if (!Application.isPlaying)
        {
            Fit();
        }
    }

    void Start()
    {
        if (Application.isPlaying)
        {
            Fit();
        }
    }

    void Fit()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Vector2 spriteSize = sr.bounds.size;
        Vector3 scale = transform.localScale;

        if (fitWidth)
            scale.x = screenWidth / spriteSize.x * scale.x;

        if (fitHeight)
            scale.y = screenHeight / spriteSize.y * scale.y;

        transform.localScale = scale;

        if (alignToCameraBottomLeft)
        {
            Vector3 camBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            float halfW = sr.bounds.size.x * transform.localScale.x / 2f;
            float halfH = sr.bounds.size.y * transform.localScale.y / 2f;

            transform.position = new Vector3(
                camBottomLeft.x + halfW,
                camBottomLeft.y + halfH,
                transform.position.z
            );
        }
    }
}
