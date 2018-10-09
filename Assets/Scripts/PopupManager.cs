using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{

    public GameObject gameStateManager;
    public GameObject popupsBackground;
    public GameObject winPopup;
    public GameObject losePopup;
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

    public void Show(GameObject popup)
    {
        popupsBackground.SetActive(true);
        popup.SetActive(true);
    }



    public void Hide(GameObject popup)
    {
        popupsBackground.SetActive(false);
        popup.SetActive(false);
    }
}
