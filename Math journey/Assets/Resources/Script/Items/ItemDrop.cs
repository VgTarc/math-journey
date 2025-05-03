using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float dropForce = 5;

    private void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomforce = Random.Range(dropForce * 0.5f, dropForce * 1.2f);
        itemRb.AddForce(randomDirection * dropForce, ForceMode2D.Impulse);
    }


}
