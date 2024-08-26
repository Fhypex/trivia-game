using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;


public class SqLiteDriver : SelfSingleton<SqLiteDriver>, IDatabaseDriver<IDbConnection>
{
    private string connectionString;
    private IDbConnection dbConnection;

    public string getConnectionString()
    {
        return connectionString;
    }

    public void setConnectionString(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public void OpenConnection()
    {
        dbConnection = CreateAndOpenDatabase();
    }

    public IDbConnection Connection()
    {
        return dbConnection;
    }
    public void CloseConnection()
    {
        dbConnection.Close();
    }


    public IDbCommand CreateCommand()
    {
        return dbConnection.CreateCommand();
    }



    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
        string dbUri = "URI=file:toudb.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        using (IDbCommand command = dbConnection.CreateCommand())
        {
            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();
        }

        Debug.Log("Database connection state: " + dbConnection.State);

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateSelectableQuestionsTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateSelectableQuestionsTable.CommandText = "CREATE TABLE IF NOT EXISTS multiple_choice_questions (" +
                                           "ID TEXT PRIMARY KEY NOT NULL, " +
                                           "CAPTION TEXT NOT NULL," +
                                           "CATEGORY TEXT NOT NULL," +
                                           "DIFFICULTY TEXT NOT NULL," +
                                           "OPTION_COUNT INTEGER NOT NULL," +
                                           "ANSWER_ID INTEGER NOT NULL" +
                                           ");"; 
        dbCommandCreateSelectableQuestionsTable.ExecuteReader();


        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateSelectableOptionsTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateSelectableOptionsTable.CommandText = "CREATE TABLE IF NOT EXISTS multiple_choice_options (" +
                                                   "ID INTEGER NOT NULL, " +
                                                   "VALUE TEXT NOT NULL, " +
                                                   "QUESTION_ID TEXT NOT NULL, " + // Ensure this matches the type of ID in the referenced table
                                                   "FOREIGN KEY (QUESTION_ID) REFERENCES multiple_choice_questions(ID), " +
                                                   "PRIMARY KEY (ID, QUESTION_ID)" +
                                                   ");";
        dbCommandCreateSelectableOptionsTable.ExecuteReader();
        return dbConnection;
    }


}