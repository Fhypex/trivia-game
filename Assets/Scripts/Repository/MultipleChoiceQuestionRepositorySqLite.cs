using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MultipleChoiceQuestionStringRepositorySqLite : IMultipleChoiceQuestionRepository
{
    private readonly SqLiteDriver sqLiteDriver;
    private readonly MultipleChoiceMetaDataRepositorySqLite multipleChoiceMetaDataRepositorySqLite;
    private readonly MultipleChoiceOptionStringRepositorySqLite multipleChoiceOptionRepositorySqLite;

    public MultipleChoiceQuestionStringRepositorySqLite(SqLiteDriver sqLiteDriver,
                                                        MultipleChoiceMetaDataRepositorySqLite multipleChoiceMetaDataRepositorySqLite,
                                                        MultipleChoiceOptionStringRepositorySqLite multipleChoiceOptionRepositorySqLite)
    {
        this.sqLiteDriver = sqLiteDriver;
        this.multipleChoiceMetaDataRepositorySqLite = multipleChoiceMetaDataRepositorySqLite;
        this.multipleChoiceOptionRepositorySqLite = multipleChoiceOptionRepositorySqLite;
    }


    public MultipleChoiceQuestionStringRepositorySqLite()
    {
        this.sqLiteDriver = SqLiteDriver.Instance;
        this.multipleChoiceMetaDataRepositorySqLite = new MultipleChoiceMetaDataRepositorySqLite();
        this.multipleChoiceOptionRepositorySqLite = new MultipleChoiceOptionStringRepositorySqLite();
    }

    public bool SaveIfNotExists(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> question)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try
            {
                QuestionEntity questionEntity = QuestionEntity.FromModel(question);
                QuestionMetaDataEntity questionMetaDataEntity = questionEntity.GetQuestionMetaDataEntity();

                if (!multipleChoiceMetaDataRepositorySqLite.SaveIfNotExists(questionMetaDataEntity))
                {
                    return false;
                }
                
                foreach (var option in questionEntity.GetQuestionOptionEntities())
                {
                    if (!multipleChoiceOptionRepositorySqLite.SaveIfNotExists(option))
                    {
                        return false;
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                transaction.Rollback();
                return false;
            }
        }
    }


    public bool Update(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> question)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try
            {
                QuestionEntity questionEntity = QuestionEntity.FromModel(question);
                QuestionMetaDataEntity questionMetaDataEntity = questionEntity.GetQuestionMetaDataEntity();

                if (!multipleChoiceMetaDataRepositorySqLite.Update(questionMetaDataEntity))
                {
                    return false;
                }

                int savedOptions = multipleChoiceOptionRepositorySqLite.CountOptionsByQuestionId(new QuestionId(question.GetId().Value()));

                if(questionEntity.GetQuestionOptionEntities().Count != savedOptions)
                {
                    multipleChoiceOptionRepositorySqLite.DropAllOptionsByQuestionId(new QuestionId(question.GetId().Value()));
                    
                    foreach (var option in questionEntity.GetQuestionOptionEntities())
                    {
                        if (!multipleChoiceOptionRepositorySqLite.SaveIfNotExists(option))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    foreach (var option in questionEntity.GetQuestionOptionEntities())
                    {
                        if (!multipleChoiceOptionRepositorySqLite.Update(option))
                        {
                            return false;
                        }
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                transaction.Rollback();
                return false;
            }
        }
    }


    public int Delete(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> question)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try
            {
                QuestionEntity questionEntity = QuestionEntity.FromModel(question);
                QuestionMetaDataEntity questionMetaDataEntity = questionEntity.GetQuestionMetaDataEntity();

                if (multipleChoiceMetaDataRepositorySqLite.Delete(questionMetaDataEntity) == -1)
                {
                    return -1;
                }

                if (!multipleChoiceOptionRepositorySqLite.DropAllOptionsByQuestionId(new QuestionId(question.GetId().Value())))
                {
                    return -1;
                }
                transaction.Commit();
                return 1;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                transaction.Rollback();
                return -1;
            }
        }
    }


    public List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> GetAll()
    {
        List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> questions = new List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>>();

        using (IDbCommand command = sqLiteDriver.CreateCommand())
        {
            command.CommandText = "SELECT * FROM multiple_choice_questions";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    QuestionMetaDataEntity questionMetaDataEntity = new QuestionMetaDataEntity(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5)
                    );

                    List<QuestionOptionEntity> questionOptionEntities = multipleChoiceOptionRepositorySqLite.GetOptionsByQuestionId(new QuestionId(questionMetaDataEntity.GetQuestionId()));

                    questions.Add(new QuestionEntity(questionMetaDataEntity, questionOptionEntities).ToModel());
                }
            }
        }
        return questions;
    }

    public List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> GetQuestionsByCategory(QuestionCategory questionCategory) {
        List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> questions = new List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>>();

        using (IDbCommand command = sqLiteDriver.CreateCommand())
        {
            command.CommandText = "SELECT * FROM multiple_choice_questions WHERE CATEGORY = @category";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@category";
            parameter.Value = questionCategory.ToString();
            command.Parameters.Add(parameter);

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    QuestionMetaDataEntity questionMetaDataEntity = new QuestionMetaDataEntity(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5)
                    );

                    List<QuestionOptionEntity> questionOptionEntities = multipleChoiceOptionRepositorySqLite.GetOptionsByQuestionId(new QuestionId(questionMetaDataEntity.GetQuestionId()));

                    questions.Add(new QuestionEntity(questionMetaDataEntity, questionOptionEntities).ToModel());
                }
            }
        }
        return questions;
    }


    public bool Exists(IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption> question) {
        using (IDbCommand command = sqLiteDriver.CreateCommand())
        {
            command.CommandText = "SELECT COUNT(*) FROM multiple_choice_questions WHERE ID = @id";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = question.GetId().Value();
            command.Parameters.Add(parameter);

            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0;
            }
        }
        return false;
    }


    public long Count()
    {
        using (IDbCommand command = sqLiteDriver.CreateCommand())
        {
            command.CommandText = "SELECT COUNT(*) FROM multiple_choice_questions";
            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count;
            }
        }
        return 0;
    }


    public List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> GetQuestionsByDifficulityRange(QuestionDifficulity startDifficulity, QuestionDifficulity endDifficulity) 
    {
        List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>> questions = new List<IMultipleChoiceQuestion<QuestionMultipleChoiceStringOption>>();

        using (IDbCommand command = sqLiteDriver.CreateCommand())
        {
            command.CommandText = "SELECT * FROM multiple_choice_questions WHERE DIFFICULTY BETWEEN @startDifficulity AND @endDifficulity";
            IDbDataParameter startDifficulityParameter = command.CreateParameter();
            startDifficulityParameter.ParameterName = "@startDifficulity";
            startDifficulityParameter.Value = startDifficulity.ToString();
            command.Parameters.Add(startDifficulityParameter);

            IDbDataParameter endDifficulityParameter = command.CreateParameter();
            endDifficulityParameter.ParameterName = "@endDifficulity";
            endDifficulityParameter.Value = endDifficulity.ToString();
            command.Parameters.Add(endDifficulityParameter);

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    QuestionMetaDataEntity questionMetaDataEntity = new QuestionMetaDataEntity(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5)
                    );

                    List<QuestionOptionEntity> questionOptionEntities = multipleChoiceOptionRepositorySqLite.GetOptionsByQuestionId(new QuestionId(questionMetaDataEntity.GetQuestionId()));

                    questions.Add(new QuestionEntity(questionMetaDataEntity, questionOptionEntities).ToModel());
                }
            }
        }
        return questions;
    }

}