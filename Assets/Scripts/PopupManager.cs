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
    public GameObject soundPlayer;
    public AudioSource popupShowSound;
    public AudioSource popupHideSound;

    public Animation _animation;

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
        soundPlayer.GetComponent<SoundPlayer>().Play(popupShowSound);
        popupsBackground.SetActive(true);
        popup.SetActive(true);
        _animation.Play("Legacy Show Animation");
    }



    public void Hide(GameObject popup)
    {
        soundPlayer.GetComponent<SoundPlayer>().Play(popupHideSound);
        _animation.Play("Legacy Hide Animation");
        StartCoroutine(HideAnimation(popup));
    }

    IEnumerator HideAnimation(GameObject popup)
    {
        while (_animation.isPlaying)
        {
            yield return null;
        }
        popupsBackground.SetActive(false);
        popup.SetActive(false);
    }
}
