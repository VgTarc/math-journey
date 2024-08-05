using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject inventoryMenu;
    private bool menuActivated;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            inventoryMenu.SetActive(false); // Close the menu
            menuActivated = false;
        }

        else if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            inventoryMenu.SetActive(true); // Open the menu
            menuActivated = true;
        }

    }
}
