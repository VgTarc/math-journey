using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CSE_CameraPan : CutsceneElementBase
{
    private CinemachineVirtualCamera vCam;
    [SerializeField] private Vector2 distanceToMove;

    public override void Execute()
    {
        vCam = cutsceneHandler.vCam;
        vCam.Follow = null;

        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalPos = vCam.transform.position;
        Vector3 targetPos =  originalPos + new Vector3(distanceToMove.x, distanceToMove.y, 0);

        float startTime =Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            vCam.transform.position = Vector3.Lerp(originalPos, targetPos, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        vCam.transform.position = targetPos;

        cutsceneHandler.PlayNextElement();

        
}
    private void OnDestroy()
        {
        StopAllCoroutines();
        }
}
