using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool isInRange;
    public GameObject Freeze;
    public UnityEvent interactAction;
    public bool hasOpen;
    
    OpenDoorCanvas OpenDoorCanvas;

    public FadeAndHide fadeAndHide;

    

    private void Start()
    {
        OpenDoorCanvas = GetComponentInParent<OpenDoorCanvas>();
        if(fadeAndHide == null)
        {
            gameObject.GetComponentInParent<FadeAndHide>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                
                interactAction.Invoke(); // fire event
                
                if (OpenDoorCanvas.alreadyOpen == true)
                {
                    OpenDoorCanvas.alreadyOpen = false;
                }
                
                Debug.Log("Event fire");
            }
        }
        else
        {
            //if(!isInRange)
            //{
            //    Cursor.lockState = CursorLockMode.None;
            //    Cursor.visible = true;
            //}
            
            OpenDoorCanvas.Canvas.SetActive(false);
            OpenDoorCanvas.alreadyOpen = false;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is now in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is not in range");
        }
    }

 

    

}
