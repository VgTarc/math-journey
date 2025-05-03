using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookCanvasManager : MonoBehaviour
{
    public GameObject canvasObject;
    public TMP_Text bookText;
    public TMP_Text titleText;
    public void ToggleCanvas(string title, string text)
    {
        if(canvasObject.activeSelf)
        {
            canvasObject.SetActive(false);
        }
        else
        {
            titleText.text = title;
            bookText.text = text;
            canvasObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (canvasObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasObject.SetActive(false);
        }
    }

}
