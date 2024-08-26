using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
    public QuestionUI Question;
    /* private GameManager _gameManager; */

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
}