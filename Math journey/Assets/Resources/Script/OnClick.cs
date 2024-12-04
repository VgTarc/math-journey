using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public bool buttonPressed;
    public GameObject gameObject1;
    public void Click()
    {
        if (buttonPressed)
        {
            gameObject.SetActive(false);
            buttonPressed = false;
        }
        else if(!buttonPressed)
        {
            gameObject.SetActive(true);
            buttonPressed = true;
        }
        
    }
}
