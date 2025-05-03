using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] itemDrops;
    public GameObject[] randomDrops;

    public void ItemDrop()
    {
        if (itemDrops.Length == 0) return;

        float spacing = 0.3f; 
        Vector3 basePos = transform.position + new Vector3(0, 0.5f, 0);

        for (int i = 0; i < itemDrops.Length; i++)
        {
            Vector3 offset = new Vector3((i - itemDrops.Length / 2f) * spacing, 0, 0); // ���Ъ�鹨���ҧ�ѹ 0.3 �Ш��
            Vector3 spawnPos = basePos + offset;
            Instantiate(itemDrops[i], spawnPos, Quaternion.identity);
        }
    }

    public void RandomDrop()
    {
        if (randomDrops.Length == 0) return;

        int randomIndex = Random.Range(0, randomDrops.Length);

        GameObject selectedItem = randomDrops[randomIndex];
        Vector3 spawnPos = transform.position + new Vector3(0, 0.5f, 0); // ��鹴�ҹ����硹���
        Instantiate(selectedItem, spawnPos, Quaternion.identity);
    }

}
