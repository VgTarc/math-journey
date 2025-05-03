using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public int monsterHp;
    private PlayerHealth playerHealth;
    private PlayerCoins playerCoins;

    
    

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
            Destroy(gameObject);
            
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
            float randomY = Random.Range(0.2f, 0.5f);
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
        Instantiate(selectedItem, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}