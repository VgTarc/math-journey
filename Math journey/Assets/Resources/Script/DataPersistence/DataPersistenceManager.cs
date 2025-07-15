using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;


    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;



    //public GameManager gameManager;


    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Found more than one data persistence manager in this scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);


        Debug.Log("File name is : " + fileName);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        if (dataHandler == null) Debug.LogError("DataHandler is null after creation");
        else Debug.Log("DataHandler create");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnsceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnsceneUnloaded;
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       

        Debug.Log("Scene Load");

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //foreach (var obj in dataPersistenceObjects)
        //{
        //    Debug.Log("Found DataPersistence object: " + obj.GetType().Name);
        //}

        LoadGame();
        


        //GameManager gameManager = FindObjectOfType<GameManager>();
        //if (gameManager != null)
        //{
        //    Debug.Log("Checking file: " + dataHandler.FileExists());

        //            if (dataHandler.FileExists())
        //            {
        //                gameManager.isLoadingFromSave = true;
        //                LoadGame();
        //            }
        //            else
        //            {
        //                gameManager.isLoadingFromSave = false;
        //                LoadGame();
        //                gameManager.InitializeNewGame();
        //            }
        //}

    }



    public void OnsceneUnloaded(Scene scene)
    {
        if (SceneManager.GetActiveScene().name != "Main_Menu") ;
            SaveGame();
    }

    //========================

    



    //=======================
    public void NewGame()
    {
        this.gameData = new GameData();
        gameData.currentSceneName = "Level01_Forest";

    }

    public void LoadGame()
    {
        

        Debug.Log($"LoadGame called for scene '{SceneManager.GetActiveScene().name}'");

       

        // to-do // load any data from file using the data handler // โหลดข้อมูลมางับ
        this.gameData = dataHandler.Load();

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // if no game data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. A new game need to be started before data can be loaded.");
            return;
            
        }

        if(dataPersistenceObjects != null)
        {
            // to-do // push the loaded data to all other script that need it // คือส่งข้อมูลที่เก็บไปให้อันอื่นแหละ เหมือนส่งค่าพลังชีวิตเงี้ย
            foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }


        }


    }

    public void SaveGame()
    {

        Debug.Log("==== SaveGame Called ====");

        // if we dont have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new Game needs to be started before data can be saved.");
            return;
        }

        

        gameData.currentSceneName = SceneManager.GetActiveScene().name;


        // to-do // pass the data to other scripts so they can update it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // to-do // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("OnApplicationQuit called");
        //this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =  FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }


    public bool IsSceneInitialized(string sceneName)
    {
        return gameData != null && gameData.scenesInitialized.ContainsKey(sceneName) && gameData.scenesInitialized[sceneName];
    }

    public void MarkSceneAsInitialized(string sceneName)
    {
        if (gameData != null)
        {
            if (!gameData.scenesInitialized.ContainsKey(sceneName))
            {
                gameData.scenesInitialized.Add(sceneName, true);
            }
            else
            {
                gameData.scenesInitialized[sceneName] = true;
            }
        }
    }

    public string GetSavedSceneName()
    {
        if (gameData != null && !string.IsNullOrEmpty(gameData.currentSceneName))
        {
            return gameData.currentSceneName;
        }
        return null;
    }

    public void DeleteSaveData()
    {
        dataHandler.Delete();
        PlayerPrefs.DeleteAll(); // ล้างข้อมูลเก่าหมด
        PlayerPrefs.Save();
    }

}
