using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{

    public int row;
    public int column;
    public int value;
    public bool isTarget;

    public Cell(int row, int column, int value, bool isTarget)
    {
        this.row = row;
        this.column = column;
        this.value = value;
        this.isTarget = isTarget;
    }

    public Cell(int row, int column, int value)
    {
        this.row = row;
        this.column = column;
        this.value = value;
    }

    public Cell(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public Cell Clone()
    {
        return new Cell(row, column, value, isTarget);
    }
}
