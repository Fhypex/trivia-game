
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IMultipleChoiceOptionRepository : IRepository<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>>

{
    int DropAllOptionsByQuestionId(QuestionId id);

    bool UpdateOptionsOnly(QuestionId id, QuestionMultipleChoiceStringOption[] options, OptionId answerId);

    bool UpdateOptionsOnly(QuestionId id, QuestionMultipleChoiceStringOption[] options);

    
}
