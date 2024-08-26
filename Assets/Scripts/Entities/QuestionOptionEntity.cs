using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuestionOptionEntity
{
    private int id;
    private string questionId;
    private string value;
    public QuestionOptionEntity( int id, string questionId,string value)
    {
        this.id = id;
        this.questionId = questionId;
        this.value = value;
    }

    public string GetQuestionId()
    {
        return questionId;
    }

    public int GetId()
    {
        return id;
    }

    public string GetValue()
    {
        return value;
    }

}