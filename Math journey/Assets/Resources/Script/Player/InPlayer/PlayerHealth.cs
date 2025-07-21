using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IDataPersistence
{

    public int maxHealth = 100;
    public int health;
    public Slider slider;

    PlayerMovement playerMovement;
    PlayerCoins playerCoins;
    Rigidbody2D rb;
    public GameObject canvasObjRes;
    public GameObject canvasObjDead;

    private bool isRespawning = false;

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
        playerMovement = GetComponent<PlayerMovement>();
        playerCoins = GetComponent<PlayerCoins>();
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
            playerMovement.enabled = false;
            RespawnPlayer();
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

    

    public void RespawnPlayer()
    {
        if (isRespawning) return;
        isRespawning = true;

        if (playerCoins.Coin >= 5)
        {
            StartCoroutine(ReturnGameCoroutine());
        }
        else
        {
            StartCoroutine(ResetGameCoroutine());
        }

        
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
    }

    private IEnumerator ResetGameCoroutine()
    {
        canvasObjDead.SetActive(true);

        yield return new WaitForSeconds(5f); // แสดงหน้าตาย
        DataPersistenceManager.Instance.DeleteSaveData();
        SceneManager.LoadScene("Main_Menu");

        canvasObjDead.SetActive(false);
        isRespawning = false;
    }

    private IEnumerator ReturnGameCoroutine()
    {
        canvasObjRes.SetActive(true);
        playerCoins.Coin -= 5; // Deduct 5 coins for respawn
        yield return new WaitForSeconds(2.5f); // รอ 2 วินาที
        float x = PlayerPrefs.GetFloat("CheckpointX");
        float y = PlayerPrefs.GetFloat("CheckpointY");
        float z = PlayerPrefs.GetFloat("CheckpointZ");
        transform.position = new Vector3(x, y, z);
        yield return new WaitForSeconds(2.5f);
        canvasObjRes.SetActive(false);
        playerMovement.enabled = true;
        health = maxHealth / 2;
        isRespawning = false;
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
   
