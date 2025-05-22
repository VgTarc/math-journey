using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CSE_CameraZoom : CutsceneElementBase , IDataPersistence
{
    [SerializeField] private float targetFOV;
    [SerializeField] Transform target;
    [SerializeField] private Vector3 offset;

    private CinemachineVirtualCamera vCam;
    private Transform playerTransform;
    //public GameObject obj;

    //Player Freeze
    private PlayerMovement playerMove;

    //Save- load
    public string csID;
    public bool hasTrigger;

    [ContextMenu("generate CS GUID")]
    private void GenerateGuid()
    {
        csID = System.Guid.NewGuid().ToString();
    }




    public override void Execute()
    {

        //Find player movement script
        playerMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        vCam = cutsceneHandler.vCam;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // ค้นหาตัวผู้เล่น
        vCam.Follow = null;
        Animator playerAnimator = playerMove.GetComponent<Animator>();
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0f);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMove.enabled = false;
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
        playerMove.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hasTrigger = true;
        gameObject.SetActive(false);

        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void LoadData(GameData data)
    {
        data.csTrigger.TryGetValue(csID, out hasTrigger);
        if (hasTrigger)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.csTrigger.ContainsKey(csID))
        {
            data.csTrigger.Remove(csID);
        }
        data.csTrigger.Add(csID, hasTrigger);
    }

}