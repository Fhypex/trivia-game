using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuestionCategory : IQuestionCategory<string>
{
    private string category;
    
    public string Value()
    {
        return category;
    }

    public void SetValue(string category)
    {
        this.category = category;
    }

    public QuestionCategory(string category)
    {
        this.category = category;
    }
}
