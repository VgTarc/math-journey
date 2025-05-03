using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractObj : MonoBehaviour
{

    public bool isInRange;
    public UnityEvent interactAction;
    public bool Hasinteract;
    public bool HasClaimed;

   

    public FadeAndHide fadeAndHide;

    private void Start()
    {
       if(fadeAndHide == null)
            gameObject.GetComponentInParent<FadeAndHide>();

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

    void Update()
    {
        if (isInRange && HasClaimed == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                interactAction.Invoke(); // fire event
                fadeAndHide.StartFadeOut();
                HasClaimed = true;
                Hasinteract = true;
                
            }
        }
    }

    
}
