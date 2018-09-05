using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour {
	[SerializeField] public GameObject gameFieldButton;

	[HideInInspector] public Level level;

	// Use this for initialization
	void Start () {
		//instantiate buttons
        //get level parameters
        level = new Level();
        level.GenerateCells();

		//setup grid layout
		this.GetComponent<GridLayoutGroup>().constraintCount = level.columns;

		//fill the field with buttons
		for (int i = 0; i < level.rows; i++) {
			for (int j = 0; j < level.columns; j++) {
				// get cell
				CurrentCellData instantiatedCell = Instantiate (gameFieldButton, this.transform).GetComponent<CurrentCellData> ();

				//add value
				instantiatedCell.row = i;
				instantiatedCell.column = j;
				instantiatedCell.value = level.cells [i, j].value;

				//add used flag
				instantiatedCell.isUsed = level.cells[i,j].isUsed;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
