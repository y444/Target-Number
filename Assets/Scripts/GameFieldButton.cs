using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldButton : MonoBehaviour {

	public int row;
	public int column;
	public GameObject gameplayManager;
	public ButtonState buttonState;
	public Sprite normalImage;
	public Sprite usedImage;
	public Sprite zeroImage;
	public Sprite deadImage;
	public Color32 normalTextColor;
	public Color32 usedTextColor;
	public Color32 zeroTextColor;
	public Color32 deadTextColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Get the current state of the button
		buttonState = gameplayManager.GetComponent<GameplayManager> ().GetButtonState (row, column);

		// Change image depending on the current state of the button
		SetState(buttonState);

		// Change text on the button
		SetText(gameplayManager.GetComponent<GameplayManager> ().GetButtonText (row, column) );
	}

	public void SetState(ButtonState buttonState) {
		if (buttonState == ButtonState.used) {
			this.GetComponent<Image> ().sprite = usedImage;
		}

		else if (buttonState == ButtonState.dead) {
			this.GetComponent<Image> ().sprite = deadImage;
		}

		else if (buttonState == ButtonState.zero) {
			this.GetComponent<Image> ().sprite = zeroImage;
		}

		else {
			this.GetComponent<Image> ().sprite = normalImage;
		}
	}

	public void SetText(string text) {
		if (this.buttonState == ButtonState.used) {
			this.GetComponentInChildren<Text> ().color = usedTextColor;
		}

		else if (this.buttonState == ButtonState.dead) {
			this.GetComponentInChildren<Text> ().color = deadTextColor;
		}

		else if (this.buttonState == ButtonState.zero) {
			this.GetComponentInChildren<Text> ().color = zeroTextColor;
		}

		else {
			this.GetComponentInChildren<Text> ().color = normalTextColor;
		}

		this.GetComponentInChildren<Text> ().text = text;
	}
}
