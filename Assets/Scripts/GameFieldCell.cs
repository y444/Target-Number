using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldCell : MonoBehaviour
{

    public GameObject gameplayManager;
    public GameObject valueText;
    public GameObject targetMark;

    public int row;
    public int column;
    public int value;
    public bool isTarget;

    public bool isUsed;
    public bool isZero;
    public bool isDead;

    public Sprite normalSprite;
    public Color32 normalCellColor;
    public Color32 normalTextColor;
    public Sprite usedSprite;
    public Color32 usedCellColor;
    public Color32 usedTextColor;
    public Color32 zeroCellColor;
    public Color32 zeroTextColor;
    public Color32 deadCellColor;
    public Color32 deadTextColor;


    // Use this for initialization
    void Start()
    {
        UpdateLook();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLook()
    {
        // Update value
        valueText.GetComponent<Text>().text = value.ToString();

        // Set sprites, sprite colors and text colors
        if (isUsed == true)
        {
            GetComponent<Image>().sprite = usedSprite;
            GetComponent<Image>().color = usedCellColor;
            valueText.GetComponent<Text>().color = usedTextColor;

            //Also move text lower to match the sprite. We do it only here, because a used cell can't change its look further
            Vector2 newPosition = valueText.GetComponent<Text>().rectTransform.anchoredPosition;
            newPosition.y = -4f;
            valueText.GetComponent<Text>().rectTransform.anchoredPosition = newPosition;
        }
        else if (isZero == true)
        {
            GetComponent<Image>().sprite = normalSprite;
            GetComponent<Image>().color = zeroCellColor;
            valueText.GetComponent<Text>().color = zeroTextColor;
        }
        else if (isDead == true)
        {
            GetComponent<Image>().sprite = normalSprite;
            GetComponent<Image>().color = deadCellColor;
            valueText.GetComponent<Text>().color = deadTextColor;
        }
        else
        {
            GetComponent<Image>().sprite = normalSprite;
            GetComponent<Image>().color = normalCellColor;
            valueText.GetComponent<Text>().color = normalTextColor;
        }

        //Display target sprite on target cells
        if (isTarget == true)
        {
            targetMark.SetActive(true);
        }
    }

    public void Click()
    {

    }

    public void HoverOn()
    {

    }

    public void HoverOff()
    {

    }
}
