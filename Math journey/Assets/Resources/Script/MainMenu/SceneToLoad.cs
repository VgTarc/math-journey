using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // ตั้งชื่อฉากที่ต้องการเปลี่ยน

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ตรวจว่าคือ Player เท่านั้น
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
