using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public  GameModel Configuration;

    public  QuestionModel GetQuestionForCategory(string categoryName){

    
     
        CategoryModel category = Configuration.Categories.FirstOrDefault(category => category.name == categoryName);
        if(category != null){
            return category.Questions[0];
        }
        return null;
    }
}
