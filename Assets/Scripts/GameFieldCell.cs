using UnityEngine;
using UnityEngine.UI;

public class GameFieldCell : MonoBehaviour
{

    public GameObject gameplayManager;
    public GameObject valueText;
    public GameObject targetMark;

    public int row;
    public int column;

    int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value != value)
            {
                _value = value;
                UpdateLook();
            }
        }
    }
    public bool isTarget;

    public bool IsUsed { get; private set; }

    public bool IsZero
    {
        get
        {
            return Value == 0;
        }
    }

    bool _isDead;

    public bool IsDead
    {
        get
        {
            return _isDead;

        }
        set
        {
            if (_isDead != value)
            {
                _isDead = value;
                UpdateLook();
            }
        }
    }

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

    public void UpdateLook()
    {
        // Update value
        valueText.GetComponent<Text>().text = Value.ToString();

        // Set sprites, sprite colors and text colors
        if (IsUsed)
        {
            GetComponent<Image>().sprite = usedSprite;
            GetComponent<Image>().color = usedCellColor;
            valueText.GetComponent<Text>().color = usedTextColor;

            //Also move text lower to match the sprite. We only do it here, because a used cell can't change its look further
            Vector2 newPosition = valueText.GetComponent<Text>().rectTransform.anchoredPosition;
            newPosition.y = -4f;
            valueText.GetComponent<Text>().rectTransform.anchoredPosition = newPosition;
        }
        else if (IsZero)
        {
            GetComponent<Image>().sprite = normalSprite;
            GetComponent<Image>().color = zeroCellColor;
            valueText.GetComponent<Text>().color = zeroTextColor;
        }
        else if (IsDead)
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
        if (isTarget)
        {
            targetMark.SetActive(true);
        }
    }

    public void Click()
    {
        if (IsUsed == false && IsZero == false)
        {
            IsUsed = true;
            gameplayManager.GetComponent<GameplayManager>().OnClicked(row, column);
            UpdateLook();
        }
    }

    public void HoverOn()
    {

    }

    public void HoverOff()
    {

    }
}
