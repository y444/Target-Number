using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameObject breadcrumbsPanel;
    public GameObject crumbPrefab;
    public GameObject[] crumbs;
    public GameObject prevButton;
    public Text nextButtonText;
    public Text stepText;
    public VideoPlayer videoPlayer;
    public int currentStep;
    public Color32 activeCrumbColor;
    public Color32 inactiveCrumbColor;
    public TutorialStep[] steps;
    
    public void Start()
    {
        //Initialize crumbs array
        crumbs = new GameObject[steps.Length];

        //Set breadcrumbs for correct amount of steps
        for (int i = 0; i < steps.Length; i++)
        {
            crumbs[i] = Instantiate(crumbPrefab, breadcrumbsPanel.transform);
            crumbs[i].name = "Crumb " + i.ToString();
        }

        //Set to first step
        SetCurrentStep(0);
    }

    public void UpdateButtonsLook()
    {
        if (currentStep == 0)
        {
            prevButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
        }

        if (currentStep == steps.Length - 1)
        {
            nextButtonText.text = "PLAY";
        }
        else
        {
            nextButtonText.text = "NEXT";
        }
    }

    public void UpdateCrumbsLook()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            if (i == currentStep)
            {
                crumbs[i].GetComponent<Image>().color = activeCrumbColor;
            }
            else
            {
                crumbs[i].GetComponent<Image>().color = inactiveCrumbColor;
            }
        }
    }

    public void SetCurrentStep(int step)
    {
        currentStep = step;
        UpdateButtonsLook();
        UpdateCrumbsLook();
        stepText.text = steps[currentStep].stepDescription;
        videoPlayer.clip = steps[currentStep].stepVideo;
        videoPlayer.Play();
    }

    public void NextStepOrComplete()
    {
        if (currentStep != steps.Length - 1)
        {
            SetCurrentStep(currentStep + 1);
        }
        else
        {
            sceneLoader.LoadGameplayScene();
        }
    }

    public void PrevStep()
    {
        SetCurrentStep(currentStep - 1);
    }
}
