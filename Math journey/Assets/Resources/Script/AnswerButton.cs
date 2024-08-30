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
            
            questionSetup.ClosedCanvasAndDestroy();
        }
        else
        {
            Debug.Log("Wrong answer");
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(20);
            
            questionSetup.ClosedCanvasAndDestroy();

        }

        
        questionSetup.Start();
        
    }

}
