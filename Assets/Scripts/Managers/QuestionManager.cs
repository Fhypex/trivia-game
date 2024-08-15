using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
    
    public QuestionUI Question;
    private GameManager _gameManager;

    private QuestionModel _currentQuestion;
    public string CategoryName;

    private void Start(){
        _gameManager = GameManager.Instance;

        LoadNextQuestion();
    }


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
    }
    public bool AnswerQuestion(int Index){
        return _currentQuestion.CorrectIndex == Index;
    }
}