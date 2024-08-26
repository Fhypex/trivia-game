using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IMultipleChoiceQuestion<Option> : IQuestion<QuestionId, string, OptionId> where Option : IQuestionOption<OptionId, string>
{
    Option[] GetOptions();
    int GetOptionsCount();
    List<Option> GetOptionsAsList();
    Option GetOption(OptionId optionId);
    bool IsCorrect(OptionId optionId);
    Option GetOptionByIndex(int index);
}