using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDataPersistence
{
    public GameObject[] itemDrops;
    public GameObject[] randomDrops;


    [SerializeField] public string chestID;
    [SerializeField] public bool hasInteract = false;



    [ContextMenu("generate Chest GUID")]
    private void GenerateGuid()
    {
        chestID = System.Guid.NewGuid().ToString();
    }


    public void ItemDrop()
    {
        if (itemDrops.Length == 0) return;

        float spacing = 0.3f; 
        Vector3 basePos = transform.position + new Vector3(0, 0.5f, 0);

        for (int i = 0; i < itemDrops.Length; i++)
        {
            Vector3 offset = new Vector3((i - itemDrops.Length / 2f) * spacing, 0, 0); // แต่ละชิ้นจะห่างกัน 0.3 นะจ้ะ
            Vector3 spawnPos = basePos + offset;
            Instantiate(itemDrops[i], spawnPos, Quaternion.identity);
        }

        hasInteract = true;


    }

    public void RandomDrop()
    {
        if (randomDrops.Length == 0) return;

        int randomIndex = Random.Range(0, randomDrops.Length);

        GameObject selectedItem = randomDrops[randomIndex];
        Vector3 spawnPos = transform.position + new Vector3(0, 0.5f, 0); // ขึ้นด้านบนเล็กน้อย
        Instantiate(selectedItem, spawnPos, Quaternion.identity);
    }



    public void LoadData(GameData data)
    {
        data.chestInteract.TryGetValue(chestID, out hasInteract);
        if (hasInteract)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.chestInteract.ContainsKey(chestID))
        {
            data.chestInteract.Remove(chestID);
        }
        data.chestInteract.Add(chestID, hasInteract);
    }
}
