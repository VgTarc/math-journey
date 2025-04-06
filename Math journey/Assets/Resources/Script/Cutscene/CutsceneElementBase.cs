using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    private CutsceneHandler cutsceneHandler;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute()
    {
        // เรียกใช้ Coroutine ที่นี่
        StartCoroutine(WaitAndAdvance());
    }

    protected IEnumerator WaitAndAdvance() // เปลี่ยน IEnumerable เป็น IEnumerator
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}