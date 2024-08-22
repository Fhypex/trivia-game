using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl; // For Button
using UnityEngine.SceneManagement;  

public class MathQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;
    public TextMeshProUGUI questionText;       // Assign in the Inspector
    public TextMeshProUGUI answerInputField;    // Assign in the Inspector
    public Button submitButton;                // Assign in the Inspector
    public TextMeshProUGUI feedbackText;       // Optional, for feedback

    private int currentQuestionNumber = 1;
    private int correctAnswer;
    private int score = 0;
    private int streak = 1;

    void Start()
    {
        GenerateMathQuestion();
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void GenerateMathQuestion()
    {
        // QASDWQEQWE
        int num1 = Random.Range(1, 10);  // Random number between 1 and 9
        int num2 = Random.Range(1, 10);  // Random number between 1 and 9
        correctAnswer = num1 + num2;

        questionText.text = $"What is {num1} + {num2}?";
    }

    void CheckAnswer()
    {   
        int userAnswer = FindNumberInString(answerInputField.text);
         Debug.Log("Geçersiz" + userAnswer + "devam"); 
        if (userAnswer < 20)
        {   Debug.Log("Girdi buraya ve");
            Debug.Log(userAnswer);
            if (userAnswer == correctAnswer)
            {   bool check = true;
                if(streak == 0)
                {
                    check = false;
                    streak = 1;
                }
                score += streak * 3; 
                if(check)
                {
                    streak++;
                }             
                UpdateButtonText();
                feedbackText.text = "Correct!";

            }
            else
            {   
                streak = 1;
                UpdateButtonText();
                feedbackText.text = "Incorrect. Try again!";
            }
        }
        else
        {
            feedbackText.text = "Please enter a valid number.";
            Debug.Log("Geçersiz sayı: " + answerInputField.text); 
        }

        
        answerInputField.text = string.Empty;

        currentQuestionNumber++;
        if(currentQuestionNumber <= 10)GenerateMathQuestion();
        else{
            SceneManager.LoadScene("8-ProgressSceneMenu"); 
        }
    }
    public int FindNumberInString(string inputString)
    {
         string numberString = "";

        foreach (char c in inputString)
        {
            if (char.IsDigit(c))
            {
                numberString += c;
            }
        }

        int number = 0;
        if (!string.IsNullOrEmpty(numberString))
        {
            int.TryParse(numberString, out number);
        }

    return number;
    }
    public void UpdateButtonText()
    {
        streakText.text = streak.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

}