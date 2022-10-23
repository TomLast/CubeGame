using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWave : Wave
{
    private int _normal;
    private int _cluster;
    private int _spinner;
    private float _duration;

    public StandardWave(ObstacleSpawner spawner, Map map, CoroutineHandler coroutineHandler, int normal, int cluster, int spinner, float duration) : base(spawner, map, coroutineHandler)
    {
        _normal = normal;
        _cluster = cluster;
        _spinner = spinner;
        _duration = duration;
    }

    public override void Spawn(List<Cell> cells)
    {
        _coroutineHandler.StartCoroutine(Wave(cells));
    }

    private IEnumerator Wave(List<Cell> cells)
    {
        WaveProgress = 0f;
        Queue<int> numbers = GetRandomNumbers(cells.Count);
        Dictionary<Cell, Vector3> edgeCells = GetEdgeCells(cells);

        for (int i = 0, counter = 0; counter < _spinner; i++)
        {
            int index = numbers.Dequeue();
            if (!edgeCells.ContainsKey(cells[index]))
            {
                numbers.Enqueue(index);
                continue;
            }
            counter++;
            Cell c = cells[index];

            _spawner.SpawnSpinner(c, edgeCells[c]);
        }


        for (int i = 0; i < _normal; i++)
        {
            _spawner.SpawnAsteroid(cells[numbers.Dequeue()]);
        }

        for (int i = 0; i < _cluster; i++)
        {
            _spawner.SpawnCluster(cells[numbers.Dequeue()]);
        }

        _coroutineHandler.StartCoroutine(WaveDuration());

        yield return null;
    }

    private IEnumerator WaveDuration()
    {
        WaveProgress = 0f;
        float t = 0;
        while (t < _duration)
        {
            t += Time.deltaTime;

            WaveProgress = t / _duration;

            yield return null;
        }
    }
}
