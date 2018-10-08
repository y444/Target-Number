using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickyText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickText()
    {
        Vector2 newPosition = GetComponentInChildren<Text>().rectTransform.anchoredPosition;
        newPosition.y = -4f;
        GetComponentInChildren<Text>().rectTransform.anchoredPosition = newPosition;
    }

    public void unclickText()
    {
        Vector2 newPosition = GetComponentInChildren<Text>().rectTransform.anchoredPosition;
        newPosition.y = 0f;
        GetComponentInChildren<Text>().rectTransform.anchoredPosition = newPosition;
    }
}
