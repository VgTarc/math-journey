using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterStomp : MonoBehaviour , IDataPersistence
{
    //public PlayerController playerController;
    public int damage;
    public GameObject gameObject1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.gameObject.tag == "WeakPoint")
        {

            MonsterHp monsterHp = collision.gameObject.GetComponent<MonsterHp>();
            PlayerMovement playerMovement = gameObject1.GetComponent<PlayerMovement>();

            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if (collision.transform.position.x == transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            monsterHp.TakeDamage(damage);


        }
    }
    public void LoadData(GameData data)
    {
        damage = data.stompDamage;
    }

    public void SaveData(ref GameData data)
    {
        data.stompDamage = damage;
    }
}
