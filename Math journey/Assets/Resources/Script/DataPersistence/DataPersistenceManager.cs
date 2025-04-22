using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public GameManager gameManager;


    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Found more than one data persistence manager in this scene");
        }
        Instance = this;
    }
    //========================
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        if (dataHandler.FileExists())
        {
            gameManager.isLoadingFromSave = true;
            LoadGame();
        }
        else
        {
            gameManager.isLoadingFromSave = false;
            LoadGame();
        }

        
    }
    //=======================
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // to-do // load any data from file using the data handler // โหลดข้อมูลมางับ
        this.gameData = dataHandler.Load();

        // if no game data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. initilizing data to defaults.");
            NewGame();
        }

        // to-do // push the loaded data to all other script that need it // คือส่งข้อมูลที่เก็บไปให้อันอื่นแหละ เหมือนส่งค่าพลังชีวิตเงี้ย
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        
    }

    public void SaveGame()
    {
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
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =  FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
