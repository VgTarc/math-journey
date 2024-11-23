using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorCanvas : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject InventoryMenu;
    
    public bool alreadyOpen;
    public string questionName;
    public int questionsList;


    

    public void OpenCanva()
    {

            if (alreadyOpen == false)
            {
                Canvas.SetActive(true);

                InventoryMenu.SetActive(false);

                alreadyOpen = true;

            }

            else if (alreadyOpen == true)
            {
                Canvas.SetActive(false);

                alreadyOpen = false;



            }

        }
    }

        

