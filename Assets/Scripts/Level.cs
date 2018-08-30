using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public int rows;
	public int columns;
	public int maxValue;
	public int numberOfTargets;
	[HideInInspector] public Cell[,] cells;

	// Use this for initialization
	void Start () {
		//initialize the array
		cells = new Cell[rows,columns];

		//populate the array with values
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				cells [i, j] = new Cell ();
				cells [i, j].row = i;
				cells [i, j].column = j;
				cells [i, j].value = Random.Range (0, maxValue + 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
