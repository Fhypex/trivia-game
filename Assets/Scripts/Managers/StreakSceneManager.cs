using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class StreakSceneManager : MonoBehaviour
{
    
    public void OnStreakButtonPressed()
    {
        SceneManager.LoadScene("5-StreakScene");  
    }

    public void OnQuitStreakButtonPressed()
    {
        SceneManager.LoadScene("6-StreakSceneMenu");  
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}