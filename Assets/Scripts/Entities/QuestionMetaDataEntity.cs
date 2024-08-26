

public class QuestionMetaDataEntity {

    private string questionId;
    private string caption;
    private string category;
    private string difficulity;
    private int optionsCount;
    private int answerId;

    public QuestionMetaDataEntity(string questionId, string caption, string category, string difficulity, int optionsCount, int answerId)
    {
        this.questionId = questionId;
        this.caption = caption;
        this.category = category;
        this.difficulity = difficulity;
        this.optionsCount = optionsCount;
        this.answerId = answerId;
    }

    public QuestionMetaDataEntity(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> question)
    {
        this.questionId = question.GetId().Value();
        this.caption = question.GetCaption();
        this.category = question.GetCategory().Value();
        this.difficulity = question.GetDifficulity().Value();
        this.optionsCount = question.GetOptions().Length;
        this.answerId = question.GetAnswerId().Value();
    }

    public string GetQuestionId()
    {
        return questionId;
    }

    public string GetCaption()
    {
        return caption;
    }

    public string GetCategory()
    {
        return category;
    }

    public string GetDifficulity()
    {
        return difficulity;
    }

    public int GetOptionsCount()
    {
        return optionsCount;
    }

    public int GetAnswerId()
    {
        return answerId;
    }

    public override string ToString()
    {
        return "QuestionMetaDataEntity{" +
                "questionId='" + questionId + '\'' +
                ", caption='" + caption + '\'' +
                ", category='" + category + '\'' +
                ", difficulity='" + difficulity + '\'' +
                ", optionsCount=" + optionsCount +
                ", answerId=" + answerId +
                '}';
    }

    
}