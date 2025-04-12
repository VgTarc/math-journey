using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSo : ScriptableObject
{
    public string Name;
    public string itemName;
    
    
    

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public OpenCanva openCanva = new OpenCanva();
    public bool alreadyOpen;

   

    public bool UseItem()
    {
        if(statToChange == StatToChange.health) // Heal
        {
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            
            if(playerHealth.health == playerHealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.RestoreHealth(amountToChangeStat);
                return true;
            }
            
        }


        if (openCanva == OpenCanva.Book)
        {

            OpenCanvas openCanvas = GameObject.Find(Name).GetComponent<OpenCanvas>();

            if(alreadyOpen == false)
            {
                openCanvas.OpenCanva(alreadyOpen); // false
                alreadyOpen = true;
            }

            else if(alreadyOpen == true)
            {
                openCanvas.OpenCanva(alreadyOpen); // true
                alreadyOpen = false;
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
        none,
        speed
    };

   public enum OpenCanva
    {
        none,
        Book
    }


}