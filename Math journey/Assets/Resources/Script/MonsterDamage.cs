using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime_Damage : MonoBehaviour
{

    public int damage;
    public PlayerController playerHealth;



    public GameObject parent;
  
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }

       
    }
        
}
