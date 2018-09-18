using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldButton : MonoBehaviour
{

    public GameObject gameplayManager;
    public GameObject arrows;
    public GameObject targetIndicator;
    public GameObject text;
    public int row;
    public int column;
    public ButtonState buttonState;
    public Sprite normalImage;
    public Sprite usedImage;
    public Sprite zeroImage;
    public Sprite deadImage;
    public Color32 normalTextColor;
    public Color32 usedTextColor;
    public Color32 zeroTextColor;
    public Color32 deadTextColor;


    void Start()
    {

        // Display target mark if the button is a target
        SetTarget();
    }

    void Update()
    {

        // Get the current state of the button
        GetState();

        // Change image depending on the current state of the button
        SetImage();

        // Change text on the button
        SetText();
    }

    public void GetState()
    {
        buttonState = gameplayManager.GetComponent<GameplayManager>().GetButtonState(row, column);
    }

    public void SetImage()
    {
        if (this.buttonState == ButtonState.used)
        {
            this.GetComponent<Image>().sprite = usedImage;
        }

        else if (this.buttonState == ButtonState.dead)
        {
            this.GetComponent<Image>().sprite = deadImage;
        }

        else if (this.buttonState == ButtonState.zero)
        {
            this.GetComponent<Image>().sprite = zeroImage;
        }

        else
        {
            this.GetComponent<Image>().sprite = normalImage;
        }
    }

    public void SetText()
    {
        if (this.buttonState == ButtonState.used)
        {
            text.GetComponent<Text>().color = usedTextColor;

            Vector2 usedTextPosition = text.GetComponent<RectTransform>().anchoredPosition;
            usedTextPosition.y = -4f;
            text.GetComponent<RectTransform>().anchoredPosition = usedTextPosition;

        }

        else if (this.buttonState == ButtonState.dead)
        {
            text.GetComponent<Text>().color = deadTextColor;
        }

        else if (this.buttonState == ButtonState.zero)
        {
            text.GetComponent<Text>().color = zeroTextColor;
        }

        else
        {
            text.GetComponent<Text>().color = normalTextColor;
        }

        text.GetComponent<Text>().text = gameplayManager.GetComponent<GameplayManager>().cellMatrix.cells[row, column].value.ToString();
    }

    public void SetTarget()
    {
        if (gameplayManager.GetComponent<GameplayManager>().cellMatrix.cells[row, column].isTarget == true)
        {
            targetIndicator.SetActive(true);
        }
    }

}
