using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    public CutsceneHandler cutsceneHandler { get; private set; } // ʤ�Ի����������� ���������� inspector

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute()
    {
        // ���¡�� Coroutine �����
        StartCoroutine(WaitAndAdvance());
    }

    protected IEnumerator WaitAndAdvance() // ����¹ IEnumerable �� IEnumerator
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}