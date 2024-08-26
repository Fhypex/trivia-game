using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class ProgressBarUI : MonoBehaviour
{
    public int maximumQuestion;
    public int maximumScore = 0;

    public TextMeshProUGUI score;
    public int current = 0;
    public Image Mask;
    void Start()
    {
        CalculateMaxScore();
    }

    
    void Update()
    {

        GetCurrentFill();
    }
    void GetCurrentFill(){
        current = FindNumberInString(score.text);
        float fillAmount = (float)current / (float)maximumScore;
        Mask.fillAmount = fillAmount;
    }
    public void CalculateMaxScore(){
        if(maximumScore == 0)
        for (int currentStreak = 0; currentStreak < maximumQuestion; currentStreak++)
        {   
            if(currentStreak == 0) currentStreak = 1;
            int score = currentStreak * 3;
            maximumScore += score;
            Debug.Log("Question " + currentStreak + ": " + score + " points");
        }else{
            
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
}
