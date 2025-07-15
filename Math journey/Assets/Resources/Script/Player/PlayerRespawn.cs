using UnityEngine;

public class PlayerRespawn : MonoBehaviour, IDataPersistence
{

    public float x, y, z;
    void Start()
    {
        // เช็คว่ามีตำแหน่งเซฟหรือไม่
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            x = PlayerPrefs.GetFloat("CheckpointX");
            y = PlayerPrefs.GetFloat("CheckpointY");
            z = PlayerPrefs.GetFloat("CheckpointZ");
            transform.position = new Vector3(x, y, z);

            Debug.Log("Player respawned at checkpoint: " + transform.position);
        }
    }
    public void LoadData(GameData data)
    {
        x = data.x;
        y = data.y;
        z = data.z;

    }

    public void SaveData(ref GameData data)
    {
        data.x = x;
        data.y = y;
        data.z = z;
    }

}
