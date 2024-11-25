using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterStomp : MonoBehaviour
{
    //public PlayerController playerController;
    public int damage;
    public GameObject gameObject1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.gameObject.tag == "WeakPoint")
        {

            MonsterHp monsterHp = collision.gameObject.GetComponent<MonsterHp>();
            PlayerController playerController = gameObject1.GetComponent<PlayerController>();

            playerController.KBCounter = playerController.KBTotalTime;
            if (collision.transform.position.x == transform.position.x)
            {
                playerController.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerController.KnockFromRight = false;
            }
            monsterHp.TakeDamage(damage);


        }
    }
}
