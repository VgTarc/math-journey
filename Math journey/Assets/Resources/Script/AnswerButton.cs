using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField]
    private TextMeshProUGUI answerText;

    
    public GameObject gameObject;
    
    
    

    public string GoName; // game object name
    private OpenDoorCanvas openDoorCanvas;
    public GameObject Door;
    private FadeAndHide fadeAndHide;

    private void Start()
    {
        if(openDoorCanvas == null)
            openDoorCanvas = Door.GetComponent<OpenDoorCanvas>();
        if(fadeAndHide == null)
            fadeAndHide = Door.GetComponent<FadeAndHide>();
    }


    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        GameObject gameObjectQuestion = GameObject.Find(GoName);
        QuestionSetup questionSetup = gameObjectQuestion.GetComponent<QuestionSetup>();
        

        

        if (isCorrect)
        {
            Debug.Log("Correct answer");
            
            questionSetup.RemoveCurrentQuestion();
        }
        else
        {
            Debug.Log("Wrong answer");
            PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
            
            questionSetup.AddBackCurrentQuestion();

        }

        if (questionSetup.questions.Count <= 0)
        {
            if (openDoorCanvas != null)
            {
                openDoorCanvas.hasDestroy = true;  
                fadeAndHide.StartFadeOut();
                //StartCoroutine(DelayedDeactivate(2f));
                openDoorCanvas.gameObject.SetActive(false);
                
            }
            
        }

        questionSetup.Start();



            
        
    }

    //private IEnumerator DelayedDeactivate(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //}

}
