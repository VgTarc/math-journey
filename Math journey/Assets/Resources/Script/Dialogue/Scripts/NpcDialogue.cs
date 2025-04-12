using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public AdvancedDialogueSO[] conversation;

    private Transform player;
    private SpriteRenderer speechBubble;

    private AdvancedDialogueManager advancedDialogueManager;

    private bool dialogueInitiated;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble = GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();
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
}
