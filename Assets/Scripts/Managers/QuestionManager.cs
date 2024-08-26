using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class QuestionManager : Singleton<QuestionManager>
{
    public static Action onNewQuestionLoaded;
    public QuestionUI Question;

    private MultipleChoiceQuestionWithFiveOptions _currentQuestion;
    public string CategoryName;

    private void Start(){
        /* _gameManager = GameManager.Instance; */

        LoadNextQuestion();
    }


    void LoadNextQuestion(){
        /* _currentQuestion = new QuestionModel("What is the capital of France?", new string[]{"Paris", "London", "Berlin", "Madrid"});
        if(_currentQuestion != null){
            Question.PopulateQuestion(_currentQuestion);
        } */
    }

   /*  public bool AnswerQuestion(int Index){
        return _currentQuestion.GetCorrectIndex() == Index;
    } */

    public void LoadNextQuestion(){
        int category = UnityEngine.Random.Range(0, 3); // UnityEngine.Random.Range
        switch(category){
            case 0:
                CategoryName = "Bilim";
                break;
            case 1:
                CategoryName = "Deneme";
                break;
            case 2:
                CategoryName = "Sanat";
                break;
        }
        _currentQuestion = _gameManager.GetQuestionForCategory(CategoryName);
        if(_currentQuestion != null){
            Question.PopulateQuestion(_currentQuestion);
        }
        onNewQuestionLoaded?.Invoke();
    }
    public bool AnswerQuestion(int Index){
        bool iscorrect = _currentQuestion.CorrectIndex == Index;
        if(iscorrect){
            TweenResult(CorrectImage);
        }else{
            TweenResult(WrongImage);
        }
        return iscorrect;
    }

    void TweenResult(Transform resultTransform){
        Sequence result = DOTween.Sequence();
        result.Append(resultTransform.DOScale(1,.5f).SetEase(Ease.OutBack));
        result.AppendInterval(1f);
        result.Append(resultTransform.DOScale(0,.2f).SetEase(Ease.Linear));
        result.AppendCallback(LoadNextQuestion);
    }
}