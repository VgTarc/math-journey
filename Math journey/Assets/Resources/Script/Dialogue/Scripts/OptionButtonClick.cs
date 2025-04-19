using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButtonClick : MonoBehaviour
{
    public int optionIndex;
    public AdvancedDialogueManager advancedDialogueManager;

    public void Onclick()
    {
        advancedDialogueManager.Option(optionIndex);
    }
}
