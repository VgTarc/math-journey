using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public int monsterHp;
    public PlayerController playerController;
    public GameObject gameObject1;

    public GameObject fadeTextObject;
 

    public int amount;

    private void Start()
    {
        GameObject playerobj = GameObject.Find("Player");
        playerController = playerobj.GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        monsterHp -= damage;
        if (monsterHp <= 0)
        {
            playerController.GetCoin(amount);
            OnDeath();
            Destroy(gameObject);
            
        }
    }

    private void OnDeath()
    {
        if(fadeTextObject != null)
        {
            FadeText fadeText = fadeTextObject.GetComponent<FadeText>();
            if(fadeText != null )
            {
                StartCoroutine(fadeText.FadeInOut());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            MonsterStomp monsterStomp = collision.gameObject.GetComponent<MonsterStomp>();
           
           
            TakeDamage(monsterStomp.damage);
        }
    }
}