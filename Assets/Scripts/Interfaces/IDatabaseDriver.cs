using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabaseDriver<T>
{
    string getConnectionString();

    void setConnectionString(string connectionString);

    T Connection();

    void CloseConnection();

}
