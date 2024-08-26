using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  
    private float timeRemaining = 60f; 

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= 1f;
            UpdateCountdownText();
            yield return new WaitForSeconds(1f);
        }
        CountdownEnded();
    }

    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CountdownEnded()
    {
        QuitGame();
    }

    void QuitGame()
    {
        SceneManager.LoadScene("6-StreakSceneMenu"); 
    }
}