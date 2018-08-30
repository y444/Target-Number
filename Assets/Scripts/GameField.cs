using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour {
	[SerializeField] public GameObject gameFieldButton;

	// Use this for initialization
	void Start () {
		//instantiate buttons

		//get level parameters
		int rows = GameObject.Find("Logic").GetComponent<Level>().rows;
		int columns = GameObject.Find("Logic").GetComponent<Level>().columns;
		Cell[,] cells = GameObject.Find("Logic").GetComponent<Level>().cells;

		//setup grid layout
		this.GetComponent<GridLayoutGroup>().constraintCount = rows;

		//fill the field with buttons
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				CurrentCellData instantiatedCell = Instantiate (gameFieldButton, this.transform).GetComponent<CurrentCellData> ();
				instantiatedCell.row = i;
				instantiatedCell.column = j;
				instantiatedCell.value = cells [i, j].value;
				instantiatedCell.gameObject.GetComponentInChildren<Text>().text = cells [i, j].value.ToString();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
