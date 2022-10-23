using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int CellCountPerSide;
    public float CubeSize;
    public Transform Origin;
    public Map Map;
    public GameObject Cell;

    private float cellSize;
    
    private void Awake()
    {
        GenerateCube();
        GenerateSprites();
    }

    public void GenerateCube()
    {
        Map.Origin = Origin;
        Map.CubeSize = CubeSize;
        Map.CellCountPerSide = CellCountPerSide;
        Map.CellDistance = CubeSize  / CellCountPerSide;

        Map.AddCubeSide(Vector3.forward, new CubeSide(GenerateCubeSide(Vector3.forward)));
        Map.AddCubeSide(Vector3.right, new CubeSide(GenerateCubeSide(Vector3.right)));
        Map.AddCubeSide(Vector3.back, new CubeSide(GenerateCubeSide(Vector3.back)));
        Map.AddCubeSide(Vector3.left, new CubeSide(GenerateCubeSide(Vector3.left)));
        Map.AddCubeSide(Vector3.down, new CubeSide(GenerateCubeSide(Vector3.down)));
        Map.AddCubeSide(Vector3.up, new CubeSide(GenerateCubeSide(Vector3.up)));
    }

    private void GenerateSprites()
    {
        foreach (var cubeSide in Map.CUBESIDES)
        {
            foreach (var cell in cubeSide.Value.Cells)
            {
                mbCell c = Instantiate(Cell, cell.Pos, Quaternion.identity, Origin).GetComponent<mbCell>();
                c.transform.up = cell.Normal;
                c.Cell = cell;
                cell.mbCell = c;
            }
        }
    }

    
    private List<Cell> GenerateCubeSide(Vector3 side)
    {
        List<Cell> cubeSide = new List<Cell>();
        
        GameObject localTransformHelper = new GameObject();
        
        Vector3 sideCenter = Origin.position + (side * CubeSize / 2f);

        localTransformHelper.transform.position = sideCenter;
        localTransformHelper.transform.LookAt(Origin);

        cellSize = CubeSize / CellCountPerSide;
        var right = localTransformHelper.transform.right;
        var up = localTransformHelper.transform.up;
        
        Vector3 startPos = sideCenter - (right * (CubeSize / 2f)) - (up * (CubeSize / 2f));
        startPos += right * (cellSize / 2f);
        startPos += up * (cellSize / 2f);
        
        for (int y = 0; y < CellCountPerSide; y++)
        {
            for (int x = 0; x < CellCountPerSide; x++)
            {
                Vector3 pos = startPos + right * (x * cellSize) + up * (y * cellSize);
               
                Cell cell = new Cell();
                cell.Pos = pos;
                cell.Normal = side;
                cubeSide.Add(cell);
            }
        }
        Destroy(localTransformHelper);

        return cubeSide;
    }
}

