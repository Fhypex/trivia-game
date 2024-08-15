using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerTimeBasedQuestionCountButtonPressed : MonoBehaviour
{
    [SerializeField] private SinglePlayerTimeBasedGameManager singlePlayerTimeBasedGameManager;

    public Button button;

    public TMP_InputField inputField;

    public void Start()
    {
        if (singlePlayerTimeBasedGameManager == null)
        {
        Debug.LogError("SinglePlayerTimeBasedGameManager is not assigned!");
        return;
        }
        else
        {
        Debug.Log("SinglePlayerTimeBasedGameManager is assigned!");
        }
        button.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        singlePlayerTimeBasedGameManager.SetQuestionCount(inputField.text);
        Debug.Log("Question Count: " + singlePlayerTimeBasedGameManager.GetQuestionCount().ToString());
        SceneManager.LoadScene("3-SinglePlayerTimeBasedGame");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
