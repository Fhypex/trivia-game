

using System.Collections.Generic;
using System.Linq;

public class QuestionEntity {

    private QuestionMetaDataEntity questionMetaDataEntity;
    private List<QuestionOptionEntity> questionOptionEntities;

    public QuestionEntity(QuestionMetaDataEntity questionMetaDataEntity, List<QuestionOptionEntity> questionOptionEntities)
    {
        this.questionMetaDataEntity = questionMetaDataEntity;
        this.questionOptionEntities = questionOptionEntities;
        if(questionMetaDataEntity == null)
        {
            throw new System.ArgumentException("QuestionMetaDataEntity cannot be null");
        }
        if(questionOptionEntities == null || questionOptionEntities.Count == 0)
        {
            throw new System.ArgumentException("QuestionOptionEntities cannot be null or empty");
        }
        if(questionMetaDataEntity.GetOptionsCount() != questionOptionEntities.Count)
        {
            throw new System.ArgumentException("QuestionMetaDataEntity options count must be equal to QuestionOptionEntities count");
        }
        if(questionOptionEntities.Exists(option => option == null))
        {
            throw new System.ArgumentException("QuestionOptionEntities cannot contain null values");
        }
        if(questionOptionEntities.Exists(option => option.GetQuestionId() != questionMetaDataEntity.GetQuestionId()))
        {
            throw new System.ArgumentException("QuestionOptionEntities must have the same QuestionId as QuestionMetaDataEntity");
        }
    }

    public  IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> ToModel() {
        if(questionMetaDataEntity.GetOptionsCount() == 5)
        {
            return new MultipleChoiceQuestionWithFiveOptions(
                questionMetaDataEntity.GetCaption(),
                questionOptionEntities.Select(option => new QuestionMultipleChoiceStringOption(new OptionId(option.GetId()), option.GetValue())).ToArray(),
                new QuestionCategory(questionMetaDataEntity.GetCategory()),
                new QuestionDifficulity(questionMetaDataEntity.GetDifficulity()),
                new OptionId(questionMetaDataEntity.GetAnswerId())
            );
        }
        else
        {
            throw new System.ArgumentException("QuestionMetaDataEntity options count must be 5");
        }
    }


    public static QuestionEntity FromModel(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> model)
    {
        return new QuestionEntity(
            new QuestionMetaDataEntity(
                model.GetId().Value(),
                model.GetCaption(),
                model.GetCategory().ToString(),
                model.GetDifficulity().ToString(),
                model.GetOptionsCount(),
                model.GetAnswerId().Value()
            ),
            model.GetOptions().Select(option => new QuestionOptionEntity(option.GetId().Value(), model.GetId().Value(), option.GetValue())).ToList()
        );

    }

    public QuestionMetaDataEntity GetQuestionMetaDataEntity()
    {
        return questionMetaDataEntity;
    }

    public List<QuestionOptionEntity> GetQuestionOptionEntities()
    {
        return questionOptionEntities;
    }
    
}