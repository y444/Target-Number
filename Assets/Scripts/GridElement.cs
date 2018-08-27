using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement {

	private int row;
	private int column;
	private int value;
	private bool isUsed;
	private bool isActive;
	private bool isTarget;
	private int targetValue;

	public GridElement() {

	}

	public GridElement(int r, int c, int v, bool iU, bool iA, bool iT, int tV) {
		row = r;
		column = c;
		value = v;
		isUsed = iU;
		isActive = iA;
		isTarget = iT;
		targetValue = tV;
	}

	public void SetGridElementPlace(int r, int c) {
		row = r;
		column = c;
	}

	public int GetGridElementRow()
	{
		return row;
	}

	public int GetGridElementColumn()
	{
		return column;
	}

	public void SetGridElementValue(int v) {
		value = v;
		if (value != 0) {
			SetGridElementActive(false);
		}
	}

	public int GetGridElementValue()
	{
		return value;
	}

	public void SetGridElementUsed(bool iU) {
		isUsed = iU;
	}

	public bool IsGridElementUsed()
	{
		return isUsed;
	}

	public void SetGridElementActive(bool iA) {
		isActive = iA;
	}

	public bool IsGridElementActive()
	{
		return isActive;
	}

	public void SetGridElementTarget(bool iT) {
		isTarget = iT;
	}

	public bool IsGridElementTarget()
	{
		return isTarget;
	}

	public void SetGridElementTargetValue(int tV) {
		if (isTarget == true) {
			targetValue = tV;
			}
		}

	public int GetGridElementTargetValue() {
		return targetValue;
	}
}
