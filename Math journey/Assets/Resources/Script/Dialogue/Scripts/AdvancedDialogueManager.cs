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


    //TypeWriterEffect
    [SerializeField]
    private float typingSpeed = 0.02f;

    private Coroutine typeWriterRoutine;

    private bool canContinueText = true;

    // All game canvas
    private GameObject HUDCanvas;

    //Player Freeze
    private PlayerMovement playerMove;

    //
    private bool dialogueFinished = false;

    




    // Start is called before the first frame update
    void Start()
    {

    

        HUDCanvas = GameObject.Find("CanvasGameSystem");

        //Find player movement script
        playerMove = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // Find Button
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        System.Array.Sort(optionButton, (a,b) => string.Compare(a.name, b.name));
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
        if (dialogueActivated && Input.GetKeyDown(KeyCode.Return) && canContinueText)
        {
            // ไม่ให้กด Enter ถ้ายังอยู่ใน Branch
            if (currentConversation.actors != null &&
                stepNum > 0 &&
                currentConversation.actors[Mathf.Clamp(stepNum - 1, 0, currentConversation.actors.Length - 1)] == DialogueActors.Branch)
            {
                return;
            }

            HUDCanvas.SetActive(false);
            playerMove.enabled = false;

            if (currentConversation == null || currentConversation.actors == null || stepNum >= currentConversation.actors.Length)
            {
                TurnOffDialogue();
            }
            else
            {
                PlayDialogue();
            }
        }
    }


    private void PlayDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                //Set the first button to be auto-selected
                optionButton[0].GetComponent<Button>().Select();
            }
        }

        //Keep the routine from running multiple times at the same time
        if (typeWriterRoutine != null)
            StopCoroutine(typeWriterRoutine);
        if(stepNum < currentConversation.dialogue.Length)
        {
            typeWriterRoutine = StartCoroutine(TypeWriterEffect(dialogueText.text = currentConversation.dialogue[stepNum]));
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


    public void Option(int optionNum)
    {
        foreach (GameObject button in optionButton)
            button.SetActive(false);

        AdvancedDialogueSO nextConversation = null;

        if (optionNum == 0)
            nextConversation = currentConversation.option0;
        else if (optionNum == 1)
            nextConversation = currentConversation.option1;
        else if (optionNum == 2)
            nextConversation = currentConversation.option2;
        else if (optionNum == 3)
            nextConversation = currentConversation.option3;

        if (nextConversation != null)
        {
            currentConversation = nextConversation;
            stepNum = 0;
            PlayDialogue(); // เรียกต่อได้เลย ถ้าต้องการแสดงบรรทัดถัดไปทันที
        }
        else
        {
            Debug.LogWarning($"Option {optionNum} has no dialogue assigned in {currentConversation.name}");
            TurnOffDialogue(); // หรือจะให้วนกลับก็ได้
        }
    }


    private IEnumerator TypeWriterEffect(string line)
    {
        dialogueText.text = "";
        canContinueText = false;
        yield return new WaitForSeconds(.5f);
        foreach (char letter in line.ToCharArray())
        {
            if(Input.GetKeyDown(KeyCode.Return)) // Or getkeydown(keycode.f) // or GetButtonDown("Interact")
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueText = true;
    }

    public void InitiateDialogue(NpcDialogue npcDialogue)
    {
        if (dialogueActivated) return;
        // read the array of converation we are currently stepping through
        currentConversation = npcDialogue.conversation[0];
        dialogueActivated = true;
        
    }

    public event System.Action OnDialogueEnd;

    public void TurnOffDialogue()
    {
        //when stepNum more than the actual dialogue (or equal) ... lol
        dialogueFinished = (currentConversation != null && stepNum >= currentConversation.actors.Length);
        
        stepNum = 0;
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
        optionsPanel.SetActive(false);

        //Activate HUD
        HUDCanvas.SetActive(true);

        //UnFreeze Player
        playerMove.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnDialogueEnd?.Invoke();

    }

    public bool WasDialogueFinished()
    {
        return dialogueFinished;
    }




}


public enum DialogueActors
{
    Player,
    John,
    Random,
    Branch
};