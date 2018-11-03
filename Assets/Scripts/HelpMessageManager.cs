using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMessageManager : MonoBehaviour {

    public GameObject helpPopup;
    public Text informationText;

    public void DisplayMessage(string message)
    {
        helpPopup.SetActive(true);
        informationText.text = message;
    }

    public void HideMessage()
    {
        helpPopup.SetActive(false);
    }
}
