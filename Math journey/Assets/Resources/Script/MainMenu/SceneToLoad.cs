using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // ��駪��ͩҡ����ͧ�������¹

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ��Ǩ��Ҥ�� Player ��ҹ��
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
