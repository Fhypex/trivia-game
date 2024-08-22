using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class ProgressSceneManager : MonoBehaviour
{
    
    public void OnProgressButtonPressed()
    {
        SceneManager.LoadScene("7-ProgressScene");  
    }

    public void OnQuitProgressButtonPressed()
    {
        SceneManager.LoadScene("8-ProgressSceneMenu");  
    }
    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        
        Application.Quit();
    }
    
    
}