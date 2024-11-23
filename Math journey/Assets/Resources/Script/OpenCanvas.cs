using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenCanvas : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject InventoryMenu;
    public GameObject player;
    private InventoryManager InventoryManager;
    private ItemSo itemSo;

    public bool OpenCanva(bool alreadyOpen)
    {
        if(alreadyOpen == false)
        {
            Canvas.SetActive(true);
            
            InventoryMenu.SetActive(false);
            

            return false;
        }

        else if(alreadyOpen == true)
        {
            
            Canvas.SetActive(false);
            

            return true;
        }


        return false;
    }

}

