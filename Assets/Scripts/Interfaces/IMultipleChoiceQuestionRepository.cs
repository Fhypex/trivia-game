using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IMultipleChoiceQuestionRepository : IRepository<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>>

{

    List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> GetQuestionsByCategory(QuestionCategory category);

    List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> GetQuestionsByDifficulityRange(QuestionDifficulity startDfficulity, QuestionDifficulity endDifficulity);

}
