using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CubeSide
{
    public List<Cell> Cells;

    public CubeSide(List<Cell> cells)
    {
        Cells = cells;
    }
}
