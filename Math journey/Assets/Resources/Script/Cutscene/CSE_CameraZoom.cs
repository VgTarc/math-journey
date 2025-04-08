using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CSE_CameraZoom : CutsceneElementBase
{
    [SerializeField] private float targetFOV;
    [SerializeField] Transform target;
    [SerializeField] private Vector3 offset;

    private CinemachineVirtualCamera vCam;
    private Transform playerTransform;
    public GameObject obj;

    public override void Execute()
    {
        vCam = cutsceneHandler.vCam;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // ค้นหาตัวผู้เล่น
        vCam.Follow = null;
        StartCoroutine(ZoomCamera());
    }

    private IEnumerator ZoomCamera()
    {
        Vector3 originalPosition = vCam.transform.position;
        Vector3 targetPosition = target.position + offset;

        float OriginalSize = vCam.m_Lens.OrthographicSize;
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(OriginalSize, targetFOV, t);
            vCam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        vCam.m_Lens.OrthographicSize = targetFOV;
        vCam.transform.position = targetPosition;

        yield return new WaitForSeconds(4f);

        vCam.Follow = playerTransform;
        vCam.m_Lens.OrthographicSize = OriginalSize;
        Destroy(obj);

        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}