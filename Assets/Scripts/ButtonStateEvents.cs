using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateEvents : MonoBehaviour {

	[SerializeField] public Sprite buttonPressed;
	[SerializeField] public Sprite buttonInactive;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void buttonPress() {
		gameObject.GetComponent<Image> ().sprite = buttonPressed;
		Vector2 newPosition = gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition;
		newPosition.y = -4f;
		gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition = newPosition;
		gameObject.GetComponentInChildren<Text> ().color = new Color32 (0x51, 0x96, 0x33, 0xE6);
	}
}
