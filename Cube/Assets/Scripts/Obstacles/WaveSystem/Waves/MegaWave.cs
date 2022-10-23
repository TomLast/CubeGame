using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaWave : Wave
{
    private float _waveDuration;

    public MegaWave(ObstacleSpawner spawner, Map map, CoroutineHandler coroutineHandler, float waveDuration) : base(spawner, map, coroutineHandler)
    {
        _waveDuration = waveDuration;
    }

    public override void Spawn(List<Cell> cells)
    {
        foreach (var c in cells)
        {
            _spawner.SpawnAsteroid(c);
        }
        IEnumerator ie = WaveDuration();
        _coroutineHandler.StartCoroutine(ie);
    }

    private IEnumerator WaveDuration()
    {
        WaveProgress = 0f;
        float t = 0;
        while(t < _waveDuration)
        {
            t += Time.deltaTime;

            WaveProgress = t / _waveDuration;

            yield return null;
        }
    }
}
