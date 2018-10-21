using UnityEngine;
using UnityEngine.UI;

public class GameFieldCell : MonoBehaviour
{

    public GameplayManager gameplayManager;
    public HelpMessageManager helpMessageManager;
    public SoundPlayer soundPlayer;
    public GameObject valueText;
    public GameObject targetMark;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject topArrow;
    public GameObject bottomArrow;

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

    public string targetHelpMessage;

    public Sprite normalSprite;
    public Color32 normalCellColor;
    public Color32 normalTextColor;
    public string normalHelpMessage;
    public AudioSource normalPressSound;

    public Sprite usedSprite;
    public Color32 usedCellColor;
    public Color32 usedTextColor;
    public string usedHelpMessage;
    public AudioSource usedPressSound;

    public Color32 zeroCellColor;
    public Color32 zeroTextColor;
    public string zeroHelpMessage;

    public Color32 deadCellColor;
    public Color32 deadTextColor;
    public string deadHelpMessage;

    public AudioSource arrowsOnSound;
    public AudioSource arrowsOffSound;


    // Use this for initialization
    void Start()
    {
        UpdateLook();
    }

    public void UpdateLook()
    {
        // Update value
        valueText.GetComponent<Text>().text = Value.ToString();

        // Animate
        GetComponent<Animator>().SetTrigger("cellGrowsTrigger");

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
            GetComponent<Animator>().SetTrigger("targetGrowsTrigger");
        }
    }

    bool IsInteractive()
    {
        return IsUsed == false && IsZero == false && IsDead == false;
    }

    public void Click()
    {
        if (IsInteractive())
        {
            soundPlayer.GetComponent<SoundPlayer>().Play(normalPressSound);
            IsUsed = true;
            gameplayManager.OnClicked(row, column);
            UpdateLook();
            HoverOff();
            helpMessageManager.GetComponent<HelpMessageManager>().DisplayMessage(usedHelpMessage);
        }
        else
        {
            soundPlayer.GetComponent<SoundPlayer>().Play(usedPressSound);
        }
    }

    public void HoverOn()
    {

        if (IsInteractive())
        {
            soundPlayer.GetComponent<SoundPlayer>().Play(arrowsOnSound);
            GetComponent<Animator>().SetTrigger("arrowsShowTrigger");
            leftArrow.SetActive(gameplayManager.IsCellExistAndIsAlive(row, column - 1));
            rightArrow.SetActive(gameplayManager.IsCellExistAndIsAlive(row, column + 1));
            topArrow.SetActive(gameplayManager.IsCellExistAndIsAlive(row - 1, column));
            bottomArrow.SetActive(gameplayManager.IsCellExistAndIsAlive(row + 1, column));
        }

        HoverOnHelp();
    }

    public void HoverOff()
    {
        if (IsInteractive())
        {
            soundPlayer.GetComponent<SoundPlayer>().Play(arrowsOffSound);
        }

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        topArrow.SetActive(false);
        bottomArrow.SetActive(false);

        HoverOffHelp();
    }

    void HoverOnHelp()
    {
        string helpMessage;

        if (isTarget == true)
        {
            helpMessage = targetHelpMessage;
        }
        else if (IsZero == true)
        {
            helpMessage = zeroHelpMessage;
        }
        else if (IsUsed == true)
        {
            helpMessage = usedHelpMessage;
        }
        else if (IsDead == true)
        {
            helpMessage = deadHelpMessage;
        }
        else
        {
            helpMessage = normalHelpMessage;
        }

        helpMessageManager.GetComponent<HelpMessageManager>().DisplayMessage(helpMessage);
    }

    void HoverOffHelp()
    {
        helpMessageManager.GetComponent<HelpMessageManager>().DisplayDefaultMessage();
    }
}
