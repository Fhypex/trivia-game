

using UnityEngine;

public class Configuration : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Configuration Awake");
        SqLiteDriver.Instance.setConnectionString("URI=file:toudb.sqlite");
        SqLiteDriver.Instance.OpenConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}