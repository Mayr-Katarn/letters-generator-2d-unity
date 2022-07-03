using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid
{
    public Grid(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public int col, row;

    internal void Deconstruct(out int col, out int row)
    {
        col = this.col;
        row = this.row;
    }
}
