using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour , IDataPersistence
{

    public PlayerHealth playerHealth;
    public SetPlayerPosition setPlayerPosition;
    public bool isLoadingFromSave = false;

    //Start is called before the first frame update
    public void InitializeNewGame()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            Debug.Log("No Player found in this Scene. Skipping GameManager initializion.");
            return;
        }

        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        setPlayerPosition = GameObject.FindWithTag("Player").GetComponent<SetPlayerPosition>();

        if (!isLoadingFromSave)
        {
            playerHealth.InitializePlayer();
            setPlayerPosition.SetPosition();
            //isLoadingFromSave = true;
        }
    }


    public void LoadData(GameData data)
    {
        isLoadingFromSave = data.initiate;
    }


    public void SaveData(ref GameData data)
    {
       data.initiate = isLoadingFromSave;
    }



}
