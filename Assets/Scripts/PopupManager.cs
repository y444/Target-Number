﻿using System.Collections;
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
        popupsBackground.GetComponent<Animator>().SetTrigger("popupShowTrigger");
        popup.SetActive(true);
    }



    public void Hide(GameObject popup)
    {
        soundPlayer.GetComponent<SoundPlayer>().Play(popupHideSound);
        popupsBackground.GetComponent<Animator>().SetTrigger("popupHideTrigger");
        StartCoroutine(HideAnimation(popup));
    }

    IEnumerator HideAnimation(GameObject popup)
    {
        while (popupsBackground.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hidden") == false)
        {
            Debug.Log(popupsBackground.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }
        popup.SetActive(false);
    }
}
