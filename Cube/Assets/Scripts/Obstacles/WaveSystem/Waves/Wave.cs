using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave
{
    public float WaveProgress { get; protected set; } = 1f;

    protected ObstacleSpawner _spawner;
    protected Map _map;
    protected CoroutineHandler _coroutineHandler;

    public Wave(ObstacleSpawner spawner, Map map, CoroutineHandler coroutineHandler)
    {
        _spawner = spawner;
        _map = map;
        _coroutineHandler = coroutineHandler;
    }

    public abstract void Spawn(List<Cell> cells);

    protected Queue<int> GetRandomNumbers(int amount)
    {
        Queue<int> ret = new Queue<int>();
        List<int> numbers = new List<int>();
        
        for (int i = 0; i < amount; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < amount; i++)
        {
            int r = Random.Range(0, numbers.Count);
            ret.Enqueue(numbers[r]);
            numbers.RemoveAt(r);
        }

        return ret;
    }

    protected Dictionary<Cell, Vector3> GetEdgeCells(List<Cell> cubeSide)
    {
        Dictionary<Cell, Vector3> edgeCells = new Dictionary<Cell, Vector3>();

        Vector3 forward = cubeSide[0].mbCell.transform.forward;
        Vector3 right = cubeSide[0].mbCell.transform.right;

        foreach (var c in cubeSide)
        {
            if (!_map.CheckBoundary(c.Pos + forward * _map.CellDistance, c.Normal))
            {
                edgeCells.Add(c, -forward);
            }
            else if (!_map.CheckBoundary(c.Pos - forward * _map.CellDistance, c.Normal))
            {
                edgeCells.Add(c, forward);
            }
            else if (!_map.CheckBoundary(c.Pos + right * _map.CellDistance, c.Normal))
            {
                edgeCells.Add(c, -right);
            }
            else if (!_map.CheckBoundary(c.Pos - right * _map.CellDistance, c.Normal))
            {
                edgeCells.Add(c, right);
            }
        }
        return edgeCells;
    }
}
