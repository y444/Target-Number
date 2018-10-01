using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Popups { Win, Lose, Reset, Quit };

public class PopupManager : MonoBehaviour
{

    public GameObject gameStateManager;
    public GameObject popupsBackground;
    public GameObject winPopup;
    public GameObject losePopup;
    public GameObject losePopupSubtitle;
    public GameObject resetPopup;
    public GameObject quitPopup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(Popups popup)
    {
        popupsBackground.SetActive(true);
        switch (popup)
        {
            case Popups.Win:
                winPopup.SetActive(true);
                break;
            case Popups.Lose:
                losePopup.SetActive(true);
                // Set reason for loss
                if (gameStateManager.GetComponent<GameStateManager>().currentState == GameStates.LostNoMoves)
                {
                    losePopupSubtitle.GetComponent<Text>().text = "no moves left";
                }
                if (gameStateManager.GetComponent<GameStateManager>().currentState == GameStates.LostOvershoot)
                {
                    losePopupSubtitle.GetComponent<Text>().text = "target number exceeded";
                }
                break;
            case Popups.Reset:
                resetPopup.SetActive(true);
                break;
            case Popups.Quit:
                quitPopup.SetActive(true);
                break;
            default:
                break;
        }
    }



    public void Hide(Popups popup)
    {
        popupsBackground.SetActive(false);
        switch (popup)
        {
            case Popups.Win:
                winPopup.SetActive(false);
                break;
            case Popups.Lose:
                losePopup.SetActive(false);
                break;
            case Popups.Reset:
                resetPopup.SetActive(false);
                break;
            case Popups.Quit:
                quitPopup.SetActive(false);
                break;
            default:
                break;
        }
    }
}
