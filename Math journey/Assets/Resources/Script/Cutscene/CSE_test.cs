using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_test : CutsceneElementBase
{
    public override void Execute()
    {
        Debug.Log("Executing " + gameObject.name);
        base.Execute(); // เรียก Execute ของ CutsceneElementBase
    }
}