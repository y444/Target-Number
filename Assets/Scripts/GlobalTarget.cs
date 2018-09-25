using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTarget : MonoBehaviour {

    public GameObject gameplayManager;
    public GameObject targetValueText;
    public int targetValue;

	// Use this for initialization
	void Start () {
        targetValueText.GetComponent<Text>().text = gameplayManager.GetComponent<GameplayManager>().targetValue.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
