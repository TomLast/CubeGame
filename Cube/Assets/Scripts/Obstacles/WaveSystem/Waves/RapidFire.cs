using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Wave
{
    private int _fireCount;
    private float _delay;

    public RapidFire(ObstacleSpawner spawner, Map map, CoroutineHandler coroutineHandler, int fireCount, float delay) : base(spawner, map, coroutineHandler)
    {
        _fireCount = fireCount;
        _delay = delay;
    }

    public override void Spawn(List<Cell> cells)
    {
        IEnumerator ie = Fire(cells);

        _coroutineHandler.StartCoroutine(ie);
    }

    private IEnumerator Fire(List<Cell> cells)
    {
        WaveProgress = 0f;
        float t = 0f;
        int counter = _fireCount;
        while (counter != 0)
        {
            t += Time.deltaTime;

            if (t >= _delay)
            {
                t -= _delay;
                counter--;
                _spawner.SpawnAsteroid(cells[Random.Range(0, cells.Count)]);
            }
            WaveProgress = (float)(_fireCount - counter) / _fireCount;

            yield return null;
        }
    }
}
