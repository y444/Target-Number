using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

	public int row;
	public int column;
	public int value;

	public bool isTarget;
	public bool isUsed;

	public Cell(int row, int column, int value, bool isTarget, bool isUsed) {
		this.row = row;
		this.column = column;
		this.value = value;
		this.isTarget = isTarget;
		this.isUsed = isUsed;
	}

	public Cell(int row, int column, int value) {
		this.row = row;
		this.column = column;
		this.value = value;
	}

	public Cell(int row, int column) {
		this.row = row;
		this.column = column;
	}
}
