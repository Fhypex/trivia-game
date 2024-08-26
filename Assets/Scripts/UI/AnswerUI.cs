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
   public void OnAnswerClicked(){
        bool result = /* QuestionManager.Instance.AnswerQuestion(AnswerIndex); */ true;
        if(result){
            CorrectImage.DOFade(1,.5f);
        }
        else{
            IncorrectImage.DOFade(1,.5f);
        }
        Debug.Log(result);
   }
}
