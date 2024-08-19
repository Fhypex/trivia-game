using UnityEngine;
using TMPro;
using UnityEngine.UI; // For Button

public class MathQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;       // Assign in the Inspector
    public TMP_InputField answerInputField;    // Assign in the Inspector
    public Button submitButton;                // Assign in the Inspector
    public TextMeshProUGUI feedbackText;       // Optional, for feedback

    private int correctAnswer;

    void Start()
    {
        GenerateMathQuestion();
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void GenerateMathQuestion()
    {
        int num1 = Random.Range(1, 10);  // Random number between 1 and 9
        int num2 = Random.Range(1, 10);  // Random number between 1 and 9
        correctAnswer = num1 + num2;

        questionText.text = $"What is {num1} + {num2}?";
    }

    void CheckAnswer()
    {
        int userAnswer;
        if (int.TryParse(answerInputField.text, out userAnswer))
        {
            if (userAnswer == correctAnswer)
            {
                feedbackText.text = "Correct!";
            }
            else
            {
                feedbackText.text = "Incorrect. Try again!";
            }
        }
        else
        {
            feedbackText.text = "Please enter a valid number.";
        }

        // Clear the input field after submission
        answerInputField.text = string.Empty;

        // Optionally generate a new question
        // GenerateMathQuestion();
    }
}