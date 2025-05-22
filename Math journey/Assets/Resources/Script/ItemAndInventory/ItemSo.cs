using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[CreateAssetMenu]
public class ItemSo : ScriptableObject
{
    public string Name;
    public string itemName;
    public int quantity;

    [TextArea]
    public string BookName;
    [TextArea(15,20)]
    public string BookDescription;
    public List<Sprite> bookImages = new List<Sprite> ();

    public int upgradeStompDamage;

    //Get Component naja
    private MonsterStomp monsterStomp;




    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public OpenCanva openCanva = new OpenCanva();
    public PlayerUpgrade upgrade = new PlayerUpgrade();

   

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

            if (bookImages.Count == 0)
            {
                bookCanvasManager.ToggleCanvas(BookName, BookDescription, null, null);
                return true;
            }
            if (bookImages.Count == 1)
            {
                bookCanvasManager.ToggleCanvas(BookName, BookDescription, bookImages[0], null);
                return true;
            }

            bookCanvasManager.ToggleCanvas(BookName, BookDescription, bookImages[0], bookImages[1]);
            return true;
  
        }

        if(upgrade == PlayerUpgrade.StompDamage)
        {
            monsterStomp = GameObject.Find("FeetPosition").GetComponent<MonsterStomp>();
            monsterStomp.damage += upgradeStompDamage;
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

    public enum PlayerUpgrade
    {
        none,
        Speed,
        StompDamage
    }

    public ItemType itemType;


}