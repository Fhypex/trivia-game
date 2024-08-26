using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* public class GameManager : Singleton<GameManager>
{
    private  GameModel configuration;
    private SQLiteDriver dbDriver;

    public GameManager(GameModel configuration, SQLiteDriver dbDriver)
    {
        this.configuration = configuration;
        this.dbDriver = dbDriver;
    }

    void Start()
    {
        dbDriver = new SQLiteDriver();
        dbDriver.setConnectionString("URI=file:toudb.sqlite");
        dbDriver.OpenConnection();
        Configuration = new GameModel();
        Configuration.Categories = new List<CategoryModel>();
    }

    public  QuestionModel GetQuestionForCategory(string categoryName){

        
        
        CategoryModel category = Configuration.Categories.FirstOrDefault(category => category.name == categoryName);
        int num = UnityEngine.Random.Range(0, category.Questions.Count());
        if(category != null){
            return category.Questions[num];
        }
        return null;
    }
}
 */