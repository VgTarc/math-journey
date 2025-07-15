using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameManager : MonoBehaviour
{
    public static PlayerGameManager Instance;
    bool initiate = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // เช็คว่าเคย initialize player position แล้วหรือยังสำหรับซีนนี้
        if (!DataPersistenceManager.Instance.IsSceneInitialized(currentScene))
        {
            SetPlayerPosition setPlayerPosition = GetComponent<SetPlayerPosition>();
            setPlayerPosition.SetPosition();

            // mark ว่าซีนนี้ initialize แล้ว จะไม่ถูกเซ็ตซ้ำ
            DataPersistenceManager.Instance.MarkSceneAsInitialized(currentScene);
        }

        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.InitializePlayer();
    }




    //public void LoadData(GameData data)
    //{

    //    initiate = data.initiate;


    //}

    //public void SaveData(ref GameData data)
    //{

        


    //}
}
