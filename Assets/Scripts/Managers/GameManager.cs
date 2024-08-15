using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public  GameModel Configuration;

    public  QuestionModel GetQuestionForCategory(string categoryName){

        
        
        CategoryModel category = Configuration.Categories.FirstOrDefault(category => category.name == categoryName);
        int num = UnityEngine.Random.Range(0, category.Questions.Count());
        if(category != null){
            return category.Questions[num];
        }
        return null;
    }
}
