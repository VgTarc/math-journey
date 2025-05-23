using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IDataPersistence
{

    public int maxHealth = 100;
    public int health;
    public Slider slider;


    //Start is called before the first frame update
    //void Start()
    //{
    //    health = maxHealth;
    //    slider.maxValue = maxHealth;
    //    slider.value = health;
    //}

    public void InitializePlayer()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RestoreHealth(int amount)
    {
        if (health + amount > maxHealth)
        {
            health= maxHealth;
            return;
        }
        health += amount;
        slider.value = health;
    }

    public void LoadData(GameData data)
    {
        health = data.playerHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = health;
    }



}
   
