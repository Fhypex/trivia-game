using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class AnswerUI : MonoBehaviour
{
    public Image CorrectImage;
    
    public Image IncorrectImage;
    public int AnswerIndex;

    private void OnEnable(){
        QuestionManager.onNewQuestionLoaded += ResetValues;
    }
    private void OnDisable(){
        QuestionManager.onNewQuestionLoaded -= ResetValues;
    }


   public void OnAnswerClicked(){
        bool result = QuestionManager.Instance.AnswerQuestion(AnswerIndex);
        if(result){
            CorrectImage.DOFade(1,.5f);
            
        }
        else{
            IncorrectImage.DOFade(1,.5f);
        }
        
        QuestionManager.Instance.LoadNextQuestion();
   }
   void ResetValues(){
        
        CorrectImage.DOFade(0,.2f);
        IncorrectImage.DOFade(0,.2f);
   }
}
