using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    bool hastrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset Player position to the spawn point
            PlayerHealth playerHealth =  collision.GetComponent<PlayerHealth>();
            if(!hastrigger)
            {
                hastrigger = true;
                playerHealth.TakeDamage(100);
                hastrigger = false;
            }
                
            
            Debug.Log("Player has entered the dead zone and has been reset to the spawn point.");

        }
    }
}
