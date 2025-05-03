using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCDialogue
{
    AdvancedDialogueSO[] conversation { get; }

    void OnDialogueEnd();
}
