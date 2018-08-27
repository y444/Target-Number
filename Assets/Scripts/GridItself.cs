using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItself {

	private int rows;
	private int columns;
	private int maxValue;
	private int numberOfTargets;
	private GridElement[][] elements;

	public GridItself() {
		rows = 2;
		columns = 2;
		maxValue = 9;
		numberOfTargets = 1;
	}

	public GridItself(int r, int c, int mV, int nT) {
		rows = r;
		columns = c;
		maxValue = mV;
		numberOfTargets = nT;
	}

	public void SetGridRows(int r) {
		rows = r;
	}

	public int GetGridRows() {
		return rows;
	}

	public void SetGridColumns(int c) {
		columns = c;
	}

	public int GetGridColumns() {
		return columns;
	}

	public void SetGridMaxValue(int mV) {
		maxValue = mV;
	}

	public int GetGridMaxValue() {
		return maxValue;
	}

	public void SetGridNumberOfTargets(int nT) {
		numberOfTargets = nT;
	}

	public int GetGridNumberOfTargets() {
		return numberOfTargets;
	}

	public void PopulateGrid() {
		GridElement[,] g = new GridElement[rows,columns];
		//INCOMPLETE
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				g [i, j] = new GridElement ();
				g [i, j].SetGridElementPlace (i, j);
				g [i, j].SetGridElementValue (Random.Range(0,maxValue+1));
			}
		}
	}

}

