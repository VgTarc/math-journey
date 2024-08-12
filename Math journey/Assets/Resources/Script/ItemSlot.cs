using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    //======== ITEM DATA ==============//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;


    //======== ITEM SLOT ============//

    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //========= ITEM DESCRIPTION SLOT ==========//

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // Check to see if the slot is already full

        if(isFull)
        {
            return quantity;
        }

        // Update Name
        this.itemName = itemName;

        // Update Image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        // Update Description
        this.itemDescription = itemDescription;

        // Update quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;


            //Return the Leftovers
            int extraItem = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItem;

        }

        // Update Quantity Text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;



    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }


    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable == true)
            {
                    this.quantity -= 1;
                    quantityText.text = this.quantity.ToString();
                    isFull = false;

                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }

            
            

        }

        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;
        itemName = "";
        itemDescription = "";
        itemSprite = emptySprite;

        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
        isFull = false;
    }

    public void OnRightClick()
    {
        
    }

}
