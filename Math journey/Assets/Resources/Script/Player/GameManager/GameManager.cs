using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public bool isLoadingFromSave = false;

    // Start is called before the first frame update
    void Start()
    {
        if(playerHealth == null)
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }

        if(isLoadingFromSave)
        {

        }
        else
        {
            playerHealth.InitializePlayer();
        }
    }

    
}
