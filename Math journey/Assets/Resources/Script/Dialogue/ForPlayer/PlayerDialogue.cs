using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour , IDataPersistence ,  INPCDialogue
{

    [SerializeField] private string convoID;
    [SerializeField] private bool hasTalked = false;

    [SerializeField] private AdvancedDialogueSO[] _conversation;
    public AdvancedDialogueSO[] conversation => _conversation;

    private AdvancedDialogueManager advancedDialogueManager;

    private bool dialogueInitiated = false;
    public bool isPlayerD = true;

    // Start is called before the first frame update
    void Start()
    {
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();

        advancedDialogueManager.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            advancedDialogueManager.InitiateDialogue(this, isPlayerD);
            dialogueInitiated = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Speech Bubble off
            advancedDialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
        }
    }

    public void OnDialogueEnd()
    {
        if (hasTalked) return;
        hasTalked = true;
        advancedDialogueManager.OnDialogueEnd -= OnDialogueEnd;
        gameObject.SetActive(false); // หรือ Destroy(gameObject);


    }

    [ContextMenu("generate Convo GUID")]
    private void GenerateGuid()
    {
        convoID = System.Guid.NewGuid().ToString();
    }


 



    public void LoadData(GameData data)
    {
        data.playerTalked.TryGetValue(convoID, out hasTalked);
        if (hasTalked)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.playerTalked.ContainsKey(convoID))
        {
            data.playerTalked.Remove(convoID);
        }
        data.playerTalked.Add(convoID, hasTalked);
    }


}
