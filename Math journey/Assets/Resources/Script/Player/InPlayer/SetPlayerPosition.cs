using UnityEngine;

public class SetPlayerPosition : MonoBehaviour, IDataPersistence
{
    public Vector3 defaultPosition = new Vector3(-7.851f, 0f, 0f);

    public void LoadData(GameData data)
    {
        if (data.playerPositon == new Vector3(0,0,0))
        {
            Debug.Log("No position saved, using default.");
            transform.position = defaultPosition;
        }

    }

    public void SaveData(ref GameData data)
    {
        data.playerPositon = transform.position;
    }

    // สำหรับ new game โดยไม่โหลดเซฟ
    public void SetPosition()
    {
        transform.position = defaultPosition;
        Debug.Log("Default position set.");
    }
}
