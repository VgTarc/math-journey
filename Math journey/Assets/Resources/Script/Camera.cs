using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScalePlane : MonoBehaviour
{
    private Vector2 screenresolution;
    float vertical;
    float horizontal;



    // Start is called before the first frame update
    void Start()
    {
        screenresolution = new Vector2(Screen.width, Screen.height);

        MatchObjectToScreenSize();

    }
    // once per frame
    private void Update()
    {
        
        //check if screen change size
        if(screenresolution.x != Screen.width || screenresolution.y != Screen.height)
        {
            MatchObjectToScreenSize();
            screenresolution.x = Screen.width;
            screenresolution.y = Screen.height;
        }
    }



    private void MatchObjectToScreenSize()
    {
        float ObjectHeightScale = Camera.main.orthographicSize / 10f;
        float ObjectWidthScale = ObjectHeightScale * Camera.main.aspect;
        gameObject.transform.localScale = new Vector2(ObjectWidthScale, ObjectHeightScale);
    }
}
