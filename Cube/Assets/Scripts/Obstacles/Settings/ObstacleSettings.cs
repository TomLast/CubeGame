using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings")]
public class ObstacleSettings : ScriptableObject
{
    [SerializeField] protected float _entryTime;
    [SerializeField] private int _maxPerWaveEntryWave;
    [SerializeField] private int _maxPerWaveEndGame;
    [SerializeField] private int _minPerWaveEndGame;
    [SerializeField] private FloatVariable _endGameTime;
    [SerializeField] private AnimationCurve _minMaxBoundsPerWave;

    public virtual int GetObstacleAmount(float time)
    {
        if (time < _entryTime)
            return 0;

        float minMaxBounds = _minMaxBoundsPerWave.Evaluate(Mathf.Clamp((time - _entryTime), 0, _endGameTime.Value) / (_endGameTime.Value - _entryTime));
        int currentMin = (int)(1 + minMaxBounds * (_minPerWaveEndGame - 1));
        int currentMax = (int)(_maxPerWaveEntryWave + minMaxBounds * (_maxPerWaveEndGame - _maxPerWaveEntryWave));

        return Random.Range(currentMin, currentMax + 1);
    }
}
