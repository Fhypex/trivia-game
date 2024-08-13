using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuRouter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToGameScreen()
    {
        LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("SinglePlayerTimeBasedGame");
    }
}
