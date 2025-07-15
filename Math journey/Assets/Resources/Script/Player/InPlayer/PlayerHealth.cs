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

    Rigidbody2D rb;


    //Start is called before the first frame update
    //void Start()
    //{
    //    health = maxHealth;
    //    slider.maxValue = maxHealth;
    //    slider.value = health;
    //}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (slider == null)
        {
            slider = FindObjectOfType<Slider>();
            if (slider == null)
            {
                Debug.LogWarning("PlayerHealth: Slider not found!");
            }
        }
    }

    public void InitializePlayer()
    {
        health = maxHealth;
        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
            slider.value = health;
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
        if (slider != null)
            slider.value = health;
        if (health <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(RespawnPlayer());
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

    

    public IEnumerator RespawnPlayer()
    {
        GameObject canvasObj = GameObject.Find("GameOverCanvas");
        canvasObj.SetActive(true);
        yield return new WaitForSeconds(2f); // รอ 2 วินาที
        float x = PlayerPrefs.GetFloat("CheckpointX");
        float y = PlayerPrefs.GetFloat("CheckpointY");
        float z = PlayerPrefs.GetFloat("CheckpointZ");
        transform.position = new Vector3(x, y, z);
        yield return new WaitForSeconds(2f);
        canvasObj.SetActive(false);
    }


    public void LoadData(GameData data)
    {
        health = data.playerHealth;
        if (slider == null)
        {
            slider = FindObjectOfType<Slider>();
        }

        if (slider == null)
        {
            Debug.LogWarning("Slider Still missing when load data");
        }

        slider.maxValue = maxHealth;
        slider.value = health;

    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = health;
    }



}
   
