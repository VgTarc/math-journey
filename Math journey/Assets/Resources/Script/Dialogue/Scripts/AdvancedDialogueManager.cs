using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedDialogueManager : MonoBehaviour
{
    //The NPC Dialogue we are currently stepping through
    private AdvancedDialogueSO currentConversation;
    private int stepNum = 0;
    private bool dialogueActivated;

    //UI references
    private GameObject dialogueCanvas;
    private TMP_Text actor;
    private Image portrait;
    private TMP_Text dialogueText;

    private string currentSpeaker;
    private Sprite currentPortrait;

    public ActorSO[] actorSO;

    //Button reference
    private GameObject[] optionButton;
    private TMP_Text[] optionButtonText;
    private GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Find Button
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        optionsPanel = GameObject.Find("OptionsPanel");
        optionsPanel.SetActive(false);

        //find the tmp text on the buttons
        optionButtonText = new TMP_Text[optionButton.Length];
        for (int i = 0; i < optionButtonText.Length; i++) 
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>();
        }


        // turn off all the button
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }
        

        dialogueCanvas = GameObject.Find("DialogueCanvas");
        actor = GameObject.Find("ActorText").GetComponent<TMP_Text>();
        portrait = GameObject.Find("Portrait").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();

        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActivated && Input.GetKeyDown(KeyCode.F))
        {
            //Cancle dialogue if there are no lines of dialogue remaining
            if (stepNum >= currentConversation.actors.Length)
            {
                TurnOffDialogue();
            }
            // Continue if there are still  lines of dialogue remaining
            else
            {
                PlayDialogue();
            }
        }
    }

    private void PlayDialogue()
    {
        // If it's a random NPC
        if (currentConversation.actors[stepNum] == DialogueActors.Random)
            SetActorInfo(false);
        // if it's a recurring characters
        else
            SetActorInfo(true);

        // Display Dialogue
        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        // If there's a branch
        if(currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            for (int i = 0; i < currentConversation.optionText.Length; i++)
            {
                if (currentConversation.optionText[i] == null)
                    optionButton[i].SetActive(false);
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }
            }
        }

        if(stepNum < currentConversation.dialogue.Length)
        {
            dialogueText.text = currentConversation.dialogue[stepNum];
        }
        else
        {
            optionsPanel.SetActive(true);
        }

        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetActorInfo(bool recurringCharacter)
    {
        if(recurringCharacter)
        {
            for (int i = 0; i < actorSO.Length; i++)
            {
                if (actorSO[i].name == currentConversation.actors[stepNum].ToString())
                {
                    currentSpeaker = actorSO[i].actorName;
                    currentPortrait = actorSO[i].actorPortrait;
                }
            }
        }
        else
        {
            currentSpeaker = currentConversation.randomActorName;
            currentPortrait = currentConversation.randomActorPortrait;
        }
    }

    public void InitiateDialogue(NpcDialogue npcDialogue)
    {
        // read the array of converation we are currently stepping through
        currentConversation = npcDialogue.conversation[0];
        dialogueActivated = true;
        
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
        optionsPanel.SetActive(false);
    }

}


public enum DialogueActors
{
    Player,
    John,
    Random,
    Branch
};