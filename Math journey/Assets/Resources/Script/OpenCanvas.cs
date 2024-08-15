using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenCanvas : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject InventoryMenu;
    private InventoryManager InventoryManager;

    public bool OpenCanva(bool alreadyOpen)
    {
        if(alreadyOpen == false)
        {
            Canvas.SetActive(true);
            InventoryMenu.SetActive(false);
            InventoryManager.menuActivated = false;

            return true;
        }

        else if(alreadyOpen == true)
        {
            Canvas.SetActive(false);
            return false;
        }


        return false;
    }

}

