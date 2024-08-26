using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuestionMultipleChoiceStringOption : IQuestionOption<OptionId, string>
{
    private readonly string option;
    private readonly OptionId id;

    public OptionId GetId()
    {
        return id;
    }

    public QuestionMultipleChoiceStringOption(OptionId id, string option)
    {
        this.id = id;
        this.option = option;
    }

    public string GetValue()
    {
        return option;
    }


    public string AsText()
    {
        return option;
    }

    public override string ToString()
    {
        return option;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        QuestionMultipleChoiceStringOption option = (QuestionMultipleChoiceStringOption)obj;
        return option.GetValue().Equals(this.option);
    }

    public override int GetHashCode()
    {
        return option.GetHashCode();
    }

    public string Value()
    {
        return option;
    }


}
