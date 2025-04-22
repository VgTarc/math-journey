using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour, IDataPersistence
{

    [SerializeField] private string npcID;
    [SerializeField] private bool hasTalked = false;

    public AdvancedDialogueSO[] conversation;

    private Transform player;
    private SpriteRenderer speechBubble;

    private AdvancedDialogueManager advancedDialogueManager;

    private bool dialogueInitiated = false;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble = GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();

        advancedDialogueManager.OnDialogueEnd += HandleDialogueEnd;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            //Speech Bubble On
            speechBubble.enabled = true;

            //Find the player Tranform
            player = collision.gameObject.GetComponent<Transform>();

            //Check the position and turn toward player
            if (player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }
            else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

            advancedDialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Speech Bubble off
            speechBubble.enabled = false;
            advancedDialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
;        }
    }

    private void Flip()
    {
        Vector3 CurrentScale = transform.parent.localScale;
        CurrentScale.x *= -1;
        transform.parent.localScale = CurrentScale;
    }

    private void HandleDialogueEnd()
    {
        if (hasTalked || !advancedDialogueManager.WasDialogueFinished()) return;
        else
        {
            hasTalked = true;
            advancedDialogueManager.OnDialogueEnd -= HandleDialogueEnd;
            gameObject.SetActive(false); // หรือ Destroy(gameObject);
        }
        
        
    }

    [ContextMenu("generate NPC GUID")]
    private void GenerateGuid()
    {
        npcID = System.Guid.NewGuid().ToString();
    }


    public void LoadData(GameData data)
    {
        data.npcTalked.TryGetValue(npcID, out hasTalked);
        if (hasTalked)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.npcTalked.ContainsKey(npcID))
        {
            data.npcTalked.Remove(npcID);
        }
        data.npcTalked.Add(npcID, hasTalked);
    }
    

}
