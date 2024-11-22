using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCameraMove : MonoBehaviour
{
    public int Speed = 1;
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y,-0.85f);
    }
}
