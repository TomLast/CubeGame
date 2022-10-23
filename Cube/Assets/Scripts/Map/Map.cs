using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "WorldObjects/Map")]
public class Map : ScriptableObject
{
    public int CellCountPerSide;
    public float CubeSize;
    public Transform Origin;
    public float CellDistance;

    public Dictionary<Vector3, CubeSide> CUBESIDES { get; } = new Dictionary<Vector3, CubeSide>();

    public void AddCubeSide(Vector3 side, CubeSide cubeSide)
    {
        if (CUBESIDES.ContainsKey(side))
            CUBESIDES.Remove(side);

        CUBESIDES.Add(side, cubeSide);
    }

    public bool CheckBoundary(Vector3 targetPos, Vector3 cubeSide)
    {
        Vector3 invert = new Vector3(cubeSide.x == 0 ? 1 : 0, cubeSide.y == 0 ? 1 : 0, cubeSide.z == 0 ? 1 : 0);
        Vector3 rect = new Vector3(invert.x * targetPos.x, invert.y * targetPos.y, invert.z * targetPos.z);

        if (Mathf.Abs(rect.x) > CubeSize / 2 || Mathf.Abs(rect.y) > CubeSize / 2 || Mathf.Abs(rect.z) > CubeSize / 2)
            return false;

        return true;
    }

    public Cell GetClosestCell(Vector3 side, Vector3 pos)
    {
        Cell ret = null;
        float closestDistance = 100f;

        foreach (var cell in CUBESIDES[side].Cells)
        {
            float distance = (cell.Pos - pos).magnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                ret = cell;
            }
        }


        return ret;
    }
}