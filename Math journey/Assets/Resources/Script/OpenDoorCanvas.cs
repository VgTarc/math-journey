using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorCanvas : MonoBehaviour, IDataPersistence
{
    public GameObject Canvas;
    public GameObject InventoryMenu;
    
    public bool alreadyOpen;
    public string questionName;
    public int questionsList;

    public FadeAndHide fadeAndHide;
    


    [SerializeField] public string doorID;
    [SerializeField] public bool hasDestroy = false;

    public QuestionSetup questionSetup;


    [ContextMenu("generate Door GUID")]
    private void GenerateGuid()
    {
        doorID = System.Guid.NewGuid().ToString();
    }

    

    public void OpenCanva()
    {

            if (alreadyOpen == false)
            {
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;

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

    private void Start()
    {
        if(fadeAndHide == null)
        {
            gameObject.GetComponent<FadeAndHide>();
        }
    }

    private void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        data.doorsDestroy.TryGetValue(doorID, out hasDestroy);
        if (hasDestroy)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.doorsDestroy.ContainsKey(doorID))
        {
            data.doorsDestroy.Remove(doorID);
        }
        data.doorsDestroy.Add(doorID, hasDestroy);
    }
}

        

