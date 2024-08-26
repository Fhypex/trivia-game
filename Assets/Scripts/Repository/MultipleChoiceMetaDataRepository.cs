using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MultipleChoiceMetaDataRepositorySqLite : IRepository<QuestionMetaDataEntity>
{
    private readonly SqLiteDriver sqLiteDriver;

    public MultipleChoiceMetaDataRepositorySqLite(SqLiteDriver sqLiteDriver)
    {
        this.sqLiteDriver = sqLiteDriver;
    }

    public MultipleChoiceMetaDataRepositorySqLite()
    {
        this.sqLiteDriver = SqLiteDriver.Instance;
    }

    public bool SaveIfNotExists(QuestionMetaDataEntity questionMetaDataEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "INSERT OR IGNORE INTO multiple_choice_questions" +
                              "(ID, CAPTION, CATEGORY, DIFFICULITY, OPTION_COUNT, ANSWER_ID)" +
                              "VALUES (@id, @caption, @category, @difficulity, @option_count, @answer_id)";
        
                var parameters = new (string, object)[]
                {
                    ("@id", questionMetaDataEntity.GetQuestionId()),
                    ("@caption", questionMetaDataEntity.GetCaption()),
                    ("@difficulty", questionMetaDataEntity.GetDifficulity()),
                    ("@category", questionMetaDataEntity.GetCategory()),
                    ("@option_count", questionMetaDataEntity.GetOptionsCount()),
                    ("@answer_id", questionMetaDataEntity.GetAnswerId()),
                };

                foreach (var (name, value) in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = name;
                    parameter.Value = value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }

                // Execute the command
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to insert the questionMetaDataEntity.");
                }
                    transaction.Commit();
                    return true;
            } 
            catch (Exception e)
            {
                Debug.Log(e.Message);
                transaction.Rollback();
                return false;
            }
        }
    }

    public long Count()
    {
        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT COUNT(*) FROM multiple_choice_questions";
            
            // Execute the query and retrieve the result
            object result = dbCommand.ExecuteScalar();
            if (result != null && long.TryParse(result.ToString(), out long count))
            {
                return count;
            }
        }
        return 0;
    }

    public bool Update(QuestionMetaDataEntity questionMetaDataEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "UPDATE multiple_choice_questions" +
                              "SET (CAPTION, CATEGORY, DIFFICULITY, OPTION_COUNT, ANSWER_ID)" +
                              "VALUES (@caption, @category, @difficulity, @option_count, @answer_id) WHERE ID = @id";
        
                var parameters = new (string, object)[]
                {
                    ("@id", questionMetaDataEntity.GetQuestionId()),
                    ("@caption", questionMetaDataEntity.GetCaption()),
                    ("@difficulty", questionMetaDataEntity.GetDifficulity()),
                    ("@category", questionMetaDataEntity.GetCategory()),
                    ("@option_count", questionMetaDataEntity.GetOptionsCount()),
                    ("@answer_id", questionMetaDataEntity.GetAnswerId()),
                };

                foreach (var (name, value) in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = name;
                    parameter.Value = value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }

                // Execute the command
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to update the questionMetaDataEntity.");
                }
                    transaction.Commit();
                    return true;
            } 
            catch (Exception e)
            {
                Debug.Log(e.Message);
                transaction.Rollback();
                return false;
            }
        }
    }

    public int Delete(QuestionMetaDataEntity questionMetaDataEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "DELETE FROM multiple_choice_options WHERE ID = @id";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.Value = questionMetaDataEntity.GetQuestionId();

                command.Parameters.Add(parameter);
        
                // Execute the command
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to delete the questionMetaDataEntity.");
                }
                    transaction.Commit();
                    return affectedRows;
            } 
            catch (Exception e)
            {
                Debug.Log(e.Message);
                transaction.Rollback();
                return -1;
            }
        }
    }

    public bool Exists(QuestionMetaDataEntity questionMetaDataEntityOptionEntity)
    {
        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT COUNT(*) FROM multiple_choice_questions WHERE ID = @id";
            
            // Add parameters to prevent SQL injection
            IDbDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = questionMetaDataEntityOptionEntity.GetQuestionId();
            dbCommand.Parameters.Add(parameter);

            // Execute the query and retrieve the result
            object result = dbCommand.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0;
            }
        }
        return false;
    }
   

    public List<QuestionMetaDataEntity> GetAll()
    {
        List<QuestionMetaDataEntity> questionMetaDataEntities = new();

        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT * FROM multiple_choice_options";
            
            // Execute the query and retrieve the result
            using (IDataReader reader = dbCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string caption = reader.GetString(1);
                    string category = reader.GetString(2);
                    string difficulty = reader.GetString(3);
                    int optionCount = reader.GetInt32(4);
                    int answerId = reader.GetInt32(5);

                    questionMetaDataEntities.Add(new QuestionMetaDataEntity(id, caption, category, difficulty, optionCount, answerId));
                }
            }
        }
        return questionMetaDataEntities;
    }
}