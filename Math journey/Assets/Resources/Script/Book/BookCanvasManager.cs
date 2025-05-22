using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class BookCanvasManager : MonoBehaviour
{
    //[System.Serializable]
    //public class BookPage
    //{
    //    [TextArea]
    //    public string text;
    //    public List<Sprite> images = new List<Sprite>();
    //}


    public GameObject canvasObject;
    public TMP_Text bookText;
    public TMP_Text titleText;
    public List<GameObject> imageSlots;
    //public Button nextButton;
    //public Button prevButton;

    //public List<BookPage> currentPages = new List<BookPage>();
    //public int currentPageIndex = 0;


    //private void Start()
    //{
    //    nextButton.onClick.AddListener(NextPage);
    //    nextButton.onClick.AddListener(PreviousPage);
    //}

    public void ToggleCanvas(string title, string description, Sprite Picture1, Sprite Picture2)
    {

        List<Image> images = new List<Image>();
        for (int i = 0; i < imageSlots.Count; i++)
        {
            images.Add(imageSlots[i].GetComponent<Image>());
        }
        if(canvasObject.activeSelf)
        {
            canvasObject.SetActive(false);
        }
        else
        {
            titleText.text = title;
            bookText.text = description;

            

            if (Picture1 != null)
            {
                imageSlots[0].gameObject.SetActive(true);
                images[0].sprite = Picture1;
            }
            else if (Picture1 == null)
            {
                imageSlots[0].gameObject.SetActive(false);
            }

            if (Picture2 != null)
            {
                imageSlots[1].gameObject.SetActive(true);
                images[1].sprite = Picture2;
            }
            else if (Picture2 == null)
            {
                imageSlots[1].gameObject.SetActive(false);
            }

                canvasObject.SetActive(true);
        }
    }

    //public void ShowPage(int pageIndex)
    //{
    //    if (pageIndex < 0 || pageIndex >= currentPages.Count) return;
    //    var page = currentPages[pageIndex];
    //    bookText.text = page.text;

    //    for (int i = 0; i < imageSlots.Count; i++)
    //    {
    //        if(i < page.images.Count && page.images[i] != null)
    //        {
    //            imageSlots[i].sprite = page.images[i];
    //            imageSlots[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            imageSlots[i].gameObject.SetActive(false);
    //        }
    //    }

    //    prevButton.interactable = pageIndex > 0;
    //    nextButton.interactable = pageIndex < currentPages.Count - 1;

    //}

    //public void NextPage()
    //{
    //    if(currentPageIndex < currentPages.Count - 1)
    //    {
    //        currentPageIndex++;
    //        ShowPage(currentPageIndex);
    //    }
    //}

    //public void PreviousPage()
    //{
    //    if(currentPageIndex > 0)
    //    {
    //        currentPageIndex--;
    //        ShowPage(currentPageIndex);
    //    }
    //}


    private void Update()
    {
        if (canvasObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasObject.SetActive(false);
        }
    }

}
