using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{

    [SerializeField]
    public List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    public int questionNumber;

    public GameObject canvas;


    

    public void Awake()
    {
        // Get all the question ready
        GetQuestionAssets();
    }

    public void ClosedCanvas()
    {
        canvas.SetActive(false);
    }

    
    

    // Start is called before the first frame update
    public void Start()
    {
        // Get a new question
        SelectNewQuestion();
        // Set all text and values on screen
        SetQuestionValues();
        // Set all of the answer buttons text and correct answer values
        SetAnswerValues();
    }

    

    public void GetQuestionAssets()
    {
        // Get all of the questions from the question folder
        // Not using (Just use the list) // questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions/Union/UnionDoor1"));
        
    }

    public void SelectNewQuestion()
    {
        if(questions.Count == 0)
        {

        }
        else
        {
            // Get a random value for which question to choose
            int randomQuestionIndex = Random.Range(0, questions.Count);

            // Sey the question to the random index
            currentQuestion = questions[randomQuestionIndex];

            //Remove this question from the list so it will not be repeated (until the game restart)
            questions.RemoveAt(randomQuestionIndex);
        }
        
        
    }




    public void SetQuestionValues()
    {
        // Set the question text
        questionText.text = currentQuestion.question;

    }






    public void SetAnswerValues()
    {
        // Randomize the answer button order
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        // Set up the answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;

            // If it is the correct answer, set the bool to true
            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Get a random number of the remaining choices
            int random = Random.Range(0, originalList.Count);

            // If the random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            // Add this to the new list
            newList.Add(originalList[random]);
            //Remove this choice from the original list (it has been used)
            originalList.RemoveAt(random);
        }


        return newList;
    }

    public void RemoveCurrentQuestion()
    {
        if (currentQuestion != null && questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }

        if (questions.Count <= 0)
        {
            canvas.SetActive(false);
            ClosedCanvas();
        }

    }

    public void AddBackCurrentQuestion()
    {
        if(currentQuestion != null)
        {
            questions.Add(currentQuestion);
        }
    }

}