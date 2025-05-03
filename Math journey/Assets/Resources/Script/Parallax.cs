using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float lengthX, startposX;
    private float lengthY, startposY;

    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    
    void Start()
    {
        startposX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        startposY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

 
    void FixedUpdate()
    {
       
        float distanceX = cam.transform.position.x * parallaxEffectX;
        float movementX = (cam.transform.position.x * (1 - parallaxEffectX));

        float distanceY = cam.transform.position.y * parallaxEffectY;
        float movementY = (cam.transform.position.y * (1 - parallaxEffectY));

        transform.position = new Vector3 (startposX + distanceX, startposY + distanceY, transform.position.z);
    
        if(movementX > startposX + lengthX)
        {
            startposX += lengthX;
        }

        else if(movementX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        if (movementY > startposY + lengthY)
        {
            startposY += lengthY;
        }

        else if (movementY < startposY - lengthY)
        {
            startposY -= lengthY;
        }
    }
}
