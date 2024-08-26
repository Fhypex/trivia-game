using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button testButton; // Reference to the button

    void Start()
    {
        // Add a listener to the button to call the OnTestButtonClick method when clicked
        testButton.onClick.AddListener(OnTestButtonClick);
    }

    void OnTestButtonClick()
    {
        Debug.Log("Button clicked!"); // This will log to the console when the button is clicked
    }
}