using UnityEngine;

public class DestroyDuplicateSingleton : MonoBehaviour
{
    [Tooltip("ถ้าเช็คด้วย Tag ให้ตั้งค่า tag ให้เหมือนกันทั้งหมด และเลือก Enable เช็คด้วย Tag")]
    public bool checkByTag = false;

    private void Awake()
    {
        if (checkByTag)
        {
            var duplicates = GameObject.FindGameObjectsWithTag(gameObject.tag);
            foreach (var obj in duplicates)
            {
                if (obj != this.gameObject)
                {
                    Debug.Log($"[Singleton] Duplicate tag '{gameObject.tag}' found. Destroying the new one.");
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
        else
        {
            var allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj != this.gameObject && obj.name == this.gameObject.name)
                {
                    Debug.Log($"[Singleton] Duplicate '{gameObject.name}' found. Destroying the new one.");
                    Destroy(this.gameObject);
                    return;
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
