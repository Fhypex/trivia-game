using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MultipleChoiceQuestionWithFiveOptions : IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>
{
    private readonly QuestionId id;
    private readonly string question;
    private readonly QuestionMultipleChoiceStringOption[] options = new QuestionMultipleChoiceStringOption[5];
    private readonly QuestionCategory category;
    private readonly QuestionDifficulity difficulity;
    private readonly OptionId answerId;


    public MultipleChoiceQuestionWithFiveOptions(string question, QuestionMultipleChoiceStringOption[] options, QuestionCategory category, QuestionDifficulity difficulity, OptionId answerId)
    : this(QuestionId.Generate(), question, options, category, difficulity, answerId)
    {
    }


    public MultipleChoiceQuestionWithFiveOptions(
                                             QuestionId id,      
                                             string question, 
                                             QuestionMultipleChoiceStringOption[] options,
                                             QuestionCategory category,
                                             QuestionDifficulity difficulity, 
                                             OptionId answerId)
    {
        if(id == null)
        {
            throw new ArgumentException("Id must have a value");
        }
        if(question == null || question.Length == 0)
        {
            throw new ArgumentException("Question must have a value");
        }
        if(options == null || options.Length != 5)
        {
            throw new ArgumentException("Question must have 5 options");
        }
        if(category == null)
        {
            throw new ArgumentException("Category must have a value");
        }
        if(difficulity == null)
        {
            throw new ArgumentException("Difficulity must have a value");
        }
        if(answerId == null)
        {
            throw new ArgumentException("Answer must have a value");
        }

        if(!Array.Exists(options, option => option.GetId().Value() == answerId.Value()))
        {
            throw new ArgumentException("Options must contain the answer");
        }

        this.id = id;
        this.options = options;
        this.question = question;
        this.category = category;
        this.difficulity = difficulity;
        this.answerId = answerId;
    }



    public QuestionId GetId()
    {
        return id;
    }

    public string GetCaption()
    {
        return question;
    }
    


    public QuestionMultipleChoiceStringOption[] GetOptions()
    {
        return options;
    }

    public QuestionMultipleChoiceStringOption GetAnswer()
    {
        return options.First(option => option.GetId().Equals(answerId));
    }

    public QuestionMultipleChoiceStringOption GetOption(OptionId optionId)
    {
        return options.First(option => option.GetId().Equals(optionId));
    }


    public bool IsCorrect(OptionId optionId)
    {
        return answerId.Equals(optionId);
    }


    public QuestionMultipleChoiceStringOption GetOptionByIndex(int index)
    {
        if(index < 1 || index > 5)
        {
            throw new ArgumentException("Index must be between 1 and 5");
        }
        return options[index - 1];
    }

    public List<QuestionMultipleChoiceStringOption> GetOptionsAsList()
    {
        return options.ToList();
    }


    public IQuestionCategory<string> GetCategory()
    {
        return category;
    }


    public string AsText()
    {
        return question;
    }

    public QuestionDifficulity GetDifficulity()
    {
        return difficulity;
    }

    public OptionId GetAnswerId()
    {
        return answerId;
    }

    public int GetOptionsCount()
    {
        return 5;
    }
}