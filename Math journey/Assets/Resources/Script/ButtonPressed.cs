using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    private bool buttonPressed;
    public GameObject gameObject;
    
    


    void Update()
    {
        if(buttonPressed)
        {
            gameObject.SetActive(false);
            buttonPressed = false; 
        }
    }


    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        buttonPressed = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
