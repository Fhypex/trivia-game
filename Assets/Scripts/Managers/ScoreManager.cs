using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{   
    public TextMeshProUGUI streakButton;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText; // New TextMeshProUGUI for the high score

    private int score = 0;
    private int streak = 0;

    public static int highScore = 0; // Static variable to store the high score

    void Start()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    public void _SinglePointButtonClick()
    {
        bool check = true;
        if(streak == 0)
        {
            check = false;
            streak = 1;
        }
        score += streak; 
        if(check)
        {
            streak++;
        }
        UpdateButtonText();
        CheckHighScore(); // Check and update the high score
    }

    public void _DoublePointsButtonClick()
    {
        bool check = true;
        if(streak == 0)
        {
            check = false;
            streak = 1;
        }
        score += streak * 2; 
        if(check)
        {
            streak++;
        }
        UpdateButtonText();
        CheckHighScore(); 
    }

    public void _TriplePointsButtonClick()
    {
        bool check = true;
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
        CheckHighScore(); 
    }

    public void UpdateButtonText()
    {
        streakButton.text = streak.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    private void CheckHighScore()
    {
        if(score > highScore)
        {
            highScore = score; // Update the static high score if the current score is higher
            highScoreText.text = "High Score: " + highScore.ToString(); // Update the highScoreText
        }
    }
}
