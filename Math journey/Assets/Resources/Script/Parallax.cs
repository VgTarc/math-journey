using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

 
    void FixedUpdate()
    {
       
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = (cam.transform.position.x * (1 - parallaxEffect));

        transform.position = new Vector3 (startpos + distance, transform.position.y, transform.position.z);
    
        if(movement > startpos + length)
        {
            startpos += length;
        }

        else if(movement < startpos - length)
        {
            startpos -= length;
        }
    }
}
