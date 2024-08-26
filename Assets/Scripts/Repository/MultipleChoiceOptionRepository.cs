using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MultipleChoiceOptionStringRepositorySqLite : IRepository<QuestionOptionEntity>
{
    private readonly SqLiteDriver sqLiteDriver;

    public MultipleChoiceOptionStringRepositorySqLite(SqLiteDriver sqLiteDriver)
    {
        this.sqLiteDriver = sqLiteDriver;
    }

    public MultipleChoiceOptionStringRepositorySqLite()
    {
        this.sqLiteDriver = SqLiteDriver.Instance;
    }

    public bool SaveIfNotExists(QuestionOptionEntity questionOptionEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "INSERT OR IGNORE INTO multiple_choice_options" +
                              "(ID, VALUE, QUESTION_ID)" +
                              "VALUES (@id, @value, @question_id)";
        
                var parameters = new (string, object)[]
                {
                    ("@id", questionOptionEntity.GetId()),
                    ("@value", questionOptionEntity.GetValue()),
                    ("@question_id", questionOptionEntity.GetQuestionId())
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
                    throw new Exception("Failed to insert the questionOptionEntity.");
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

    public bool Update(QuestionOptionEntity questionOptionEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "UPDATE multiple_choice_options" +
                              "SET (VALUE)" +
                              "VALUES (@value) WHERE ID = @id AND QUESTION_ID = @question_id";
        
                var parameters = new (string, object)[]
                {
                    ("@id", questionOptionEntity.GetId()),
                    ("@value", questionOptionEntity.GetValue()),
                    ("@question_id", questionOptionEntity.GetQuestionId())
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
                    throw new Exception("Failed to update the questionOptionEntity.");
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

    public int Delete(QuestionOptionEntity questionOptionEntity)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "DELETE FROM multiple_choice_options WHERE ID = @id AND QUESTION_ID = @question_id";
        
                var parameters = new (string, object)[]
                {
                    ("@id", questionOptionEntity.GetId()),
                    ("@question_id", questionOptionEntity.GetQuestionId())
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
                    throw new Exception("Failed to delete the questionOptionEntity.");
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

    public bool Exists(QuestionOptionEntity questionOptionEntityOptionEntity)
    {
        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT COUNT(*) FROM multiple_choice_options WHERE ID = @id AND QUESTION_ID = @question_id";
            
            // Add parameters to prevent SQL injection
            IDbDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = questionOptionEntityOptionEntity.GetId();
            dbCommand.Parameters.Add(parameter);

            IDbDataParameter parameter2 = dbCommand.CreateParameter();
            parameter2.ParameterName = "@question_id";
            parameter2.Value = questionOptionEntityOptionEntity.GetQuestionId();
            dbCommand.Parameters.Add(parameter2);
            
            // Execute the query and retrieve the result
            object result = dbCommand.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0;
            }
        }

        return false;
    }
   

    public List<QuestionOptionEntity> GetAll()
    {
        List<QuestionOptionEntity> questionOptionEntities = new();

        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT * FROM multiple_choice_options";
            
            // Execute the query and retrieve the result
            using (IDataReader reader = dbCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string value = reader.GetString(1);
                    string questionId = reader.GetString(2);
                    questionOptionEntities.Add(new QuestionOptionEntity(id, questionId, value));
                }
            }
        }

        return questionOptionEntities;
    }

    public int CountOptionsByQuestionId(QuestionId questionId)
    {
        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT COUNT(*) FROM multiple_choice_options WHERE QUESTION_ID = @question_id";
            
            // Add parameters to prevent SQL injection
            IDbDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = "@question_id";
            parameter.Value = questionId.Value();
            dbCommand.Parameters.Add(parameter);
            
            // Execute the query and retrieve the result
            object result = dbCommand.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count;
            }
        }

        return -1;
    }


    public bool DropAllOptionsByQuestionId(QuestionId questionId)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "DELETE FROM multiple_choice_options WHERE QUESTION_ID = @question_id";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@question_id";
                parameter.Value = questionId.Value();
                command.Parameters.Add(parameter);

                // Execute the command
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to delete options.");
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
   
   
   public List<QuestionOptionEntity> GetOptionsByQuestionId(QuestionId questionId)
   {
         List<QuestionOptionEntity> questionOptionEntities = new();
    
         using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
         {
              // Prepare the SELECT query
              dbCommand.CommandText = "SELECT * FROM multiple_choice_options WHERE QUESTION_ID = @question_id";
              
              // Add parameters to prevent SQL injection
              IDbDataParameter parameter = dbCommand.CreateParameter();
              parameter.ParameterName = "@question_id";
              parameter.Value = questionId.Value();
              dbCommand.Parameters.Add(parameter);
              
              // Execute the query and retrieve the result
              using (IDataReader reader = dbCommand.ExecuteReader())
              {
                while (reader.Read())
                {
                     int id = reader.GetInt32(0);
                     string value = reader.GetString(1);
                     string questionIdValue = reader.GetString(2);
                     questionOptionEntities.Add(new QuestionOptionEntity(id, questionIdValue, value));
                }
              }
         }
    
         return questionOptionEntities;
   }

    public int InsertAll(QuestionOptionEntity[] questionOptionEntities)
    {
        using (IDbTransaction transaction = sqLiteDriver.Connection().BeginTransaction())
        {
            try 
            {
                IDbCommand command = sqLiteDriver.CreateCommand();
                command.CommandText = "INSERT INTO multiple_choice_options" +
                              "(ID, VALUE, QUESTION_ID)" +
                              "VALUES (@id, @value, @question_id)";
        
                foreach (var questionOptionEntity in questionOptionEntities)
                {
                    var parameters = new (string, object)[]
                    {
                        ("@id", questionOptionEntity.GetId()),
                        ("@value", questionOptionEntity.GetValue()),
                        ("@question_id", questionOptionEntity.GetQuestionId())
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
                        throw new Exception("Failed to insert the questionOptionEntity.");
                    }
                }
                transaction.Commit();
                return questionOptionEntities.Length;
            } 
            catch (Exception e)
            {
                Debug.Log(e.Message);
                transaction.Rollback();
                return -1;
            }
        }
    }


    public long Count() {
        using (IDbCommand dbCommand = sqLiteDriver.CreateCommand())
        {
            // Prepare the SELECT query
            dbCommand.CommandText = "SELECT COUNT(*) FROM multiple_choice_options";
            
            // Execute the query and retrieve the result
            object result = dbCommand.ExecuteScalar();
            if (result != null && long.TryParse(result.ToString(), out long count))
            {
                return count;
            }
        }

        return -1;
    }

}