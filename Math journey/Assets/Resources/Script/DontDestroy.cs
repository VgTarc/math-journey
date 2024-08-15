using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[3];
    public int objectIndex;

    // Start is called before the first frame update
    void Awake()
    {
        if (persistentObjects[objectIndex] == null) // if there's no same object then don't destroy
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }

        
    }

    
}
