using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class ItemSo : ScriptableObject
{
    public string Name;
    public string itemName;
    public int quantity;

    public string BookName;
    public string BookDescription;
    
    
    

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public OpenCanva openCanva = new OpenCanva();

   

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
                quantity--;
                if(quantity <= 0)
                {
                    quantity = 0;
                }
                return true;
            }
            
        }


        if (openCanva == OpenCanva.Book)
        {

            BookCanvasManager bookCanvasManager = GameObject.Find("CanvasBook").GetComponent<BookCanvasManager>();
            bookCanvasManager.ToggleCanvas(BookName, BookDescription);
            return true;

           
            
            
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

    public enum ItemType
    {
        none,
        Book,
        Potion
    }

    public ItemType itemType;


}