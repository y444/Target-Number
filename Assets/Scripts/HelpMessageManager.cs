using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMessageManager : MonoBehaviour {

    public Text informationText;
    public string defaultMessage;

	void Start () {
        DisplayDefaultMessage();	
	}

    public void DisplayMessage(string message)
    {
        informationText.text = message;
    }

    public void DisplayDefaultMessage()
    {
        informationText.text = defaultMessage;
    }
}
