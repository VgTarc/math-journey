using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset Player position to the spawn point
            collision.transform.position = new Vector3(0, 0, 0); // Change this to your desired spawn point
            Debug.Log("Player has entered the dead zone and has been reset to the spawn point.");
        }
    }
}
