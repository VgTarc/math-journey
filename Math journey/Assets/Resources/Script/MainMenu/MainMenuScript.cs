using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    string sceneToLoad = "Level01_Forest";

    public void Start()
    {
        if(!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }


    public void OnNewGameClicked()
    {
        DisableMenuButton();
        // Create a new game - which will initialize our game data
        DataPersistenceManager.Instance.DeleteSaveData();

        // เริ่มเกมใหม่
        DataPersistenceManager.Instance.NewGame();

        Debug.Log("New Game Clicked");


        // Load the gameplay scene - which will in turn save the game because of
        // OnSceneUnLoaded() in the DataPersistanceManager
        SceneManager.LoadSceneAsync("Level01_Forest");


    }

    public void OnContinueGameClicked()
    {
        DisableMenuButton();
        PlayerPrefs.DeleteAll(); // ล้างข้อมูลเก่าหมด
        PlayerPrefs.Save();
        Debug.Log("Continue Clicked - Scene to load: " + sceneToLoad);



        //sceneToLoad = DataPersistenceManager.Instance.GetSavedSceneName();

  
        if(!string.IsNullOrEmpty(sceneToLoad) )
        {
            DataPersistenceManager.Instance.LoadGame();
            SceneManager.LoadSceneAsync("Level01_Forest");
        }
        else
        {
            Debug.LogWarning("No Saved Scene Found.");
            return;
        }

        
    }

    private void DisableMenuButton()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }


    


    public void Play()
    {
        SceneManager.LoadScene("Level01_Forest");
    }


    public void Quit()
    {
        Application.Quit();
    }

}
