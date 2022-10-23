using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaWave : Wave
{
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private float _waveDuration;

    private float t;

    public override void Spawn(List<Cell> cells)
    {
        foreach (var c in cells)
        {
            _spawner.SpawnAsteroid(c);
        }
        t = 0;
        _coroutineHandler.StartCoroutine(Task.Lerp(new Lerp { Time = _waveDuration }, WaveDuration));
    }

    private void WaveDuration(float dt)
    {
        WaveProgress = Mathf.Lerp(0, 1, (t += dt) / _waveDuration);
    }
}
