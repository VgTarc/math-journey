using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 1)]
public class QuestionData : ScriptableObject
{
    [TextArea(15,20)]
    public string question;
    [Tooltip("The correct answer should always be listed first.")]
    public string[] answers;
    
}

