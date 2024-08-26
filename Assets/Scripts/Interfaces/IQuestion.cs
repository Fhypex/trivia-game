using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IQuestion<Id, Category, AnswerId>
{
    Id GetId();
    string GetCaption();
    string AsText();
    QuestionDifficulity GetDifficulity();
    IQuestionCategory<Category> GetCategory();
/*     Answer GetAnswer(); */
    AnswerId GetAnswerId();
}