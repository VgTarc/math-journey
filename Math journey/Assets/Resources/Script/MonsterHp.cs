using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour , IDataPersistence
{
    public int monsterHp;
    private PlayerHealth playerHealth;
    private PlayerCoins playerCoins;


    [SerializeField] public string MonsterID;
    [SerializeField] public bool hasDefeated = false;

    


    [ContextMenu("generate Monster GUID")]
    private void GenerateGuid()
    {
        MonsterID = System.Guid.NewGuid().ToString();
    }



    //public GameObject fadeTextObject; ยังไม่ทำ


    public int amount;
    public GameObject[] itemDrops;
    public GameObject[] randomDrops; 

    private void Start()
    {
        GameObject playerobj = GameObject.Find("Player");
        playerHealth = playerobj.GetComponent<PlayerHealth>();
        playerCoins = playerobj.GetComponent<PlayerCoins>();
    }

    public void TakeDamage(int damage)
    {
        monsterHp -= damage;
        if (monsterHp <= 0)
        {
            playerCoins.GetCoin(amount);
            OnDeath();
            gameObject.SetActive(false);
            
        }
    }

    private void OnDeath()
    {
        //if(fadeTextObject != null)
        //{
        //    FadeText fadeText = fadeTextObject.GetComponent<FadeText>();
        //    if(fadeText != null )
        //    {
        //        StartCoroutine(fadeText.FadeInOut());
        //    }
        //}
        // ยังไม่ทำ
        ItemDrop();
        RandomDrop();
        hasDefeated = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            MonsterStomp monsterStomp = collision.gameObject.GetComponent<MonsterStomp>();
           
           
            TakeDamage(monsterStomp.damage);
        }
    }

    private void ItemDrop()
    {
        if (itemDrops.Length == 0) return;
        for (int i = 0; i < itemDrops.Length; i++)
        {
            float randomX = Random.Range(-1.5f, 1.5f);
            float randomY = Random.Range(0.1f, 0.3f);
            Vector3 randomOffset = new Vector3(randomX, randomY, 0);
            Vector3 spawnPos = transform.position + randomOffset;
            Instantiate(itemDrops[i], spawnPos, Quaternion.identity);
        }
    }
    private void RandomDrop()
    {
        if (randomDrops.Length == 0) return;

        Debug.Log("Test");

        int randomIndex = Random.Range(0,randomDrops.Length);

        GameObject selectedItem = randomDrops[randomIndex];
        Instantiate(selectedItem, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
    }


    public void LoadData(GameData data)
    {
        data.Monster.TryGetValue(MonsterID, out hasDefeated);
        if (hasDefeated)
        {
            gameObject.SetActive(false);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (data.Monster.ContainsKey(MonsterID))
        {
            data.Monster.Remove(MonsterID);
        }
        data.Monster.Add(MonsterID, hasDefeated);
    }
}