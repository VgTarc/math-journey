using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSo : ScriptableObject
{
    public string itemName;
    public PlayerController playerController;

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            
            if(playerController.health == playerController.maxHealth)
            {
                return false;
            }
            else
            {
                playerController.RestoreHealth(amountToChangeStat);
                return true;
            }
            
        }
        return false;

    }



    public enum StatToChange
    {
        none,
        health, 
    };

    public enum AttributeToChange
    {
        speed
    };



}