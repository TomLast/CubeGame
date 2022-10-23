//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private CoroutineHandler _coroutineHandler;

    [SerializeField] private AnimationCurve _waveThresholdCurve;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private ClusterCubeSpawner _clusterCubeSpawner;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    [SerializeField] private FloatVariable _endGameTime;
    [SerializeField] private FloatVariable _time;
    [SerializeField] private float _maxWaveThreshold;
    [SerializeField] private ObstacleSettings _normalSettings;
    [SerializeField] private ObstacleSettings _clusterSettings;
    [SerializeField] private ObstacleSettings _spinnerSettings;
    [SerializeField] private ObstacleSettings _rapidFireSettings;

    private float _waveThreshold;
    private Dictionary<CubeSide, Wave> _currentWaves;

    private void Start()
    {
        _currentWaves = new Dictionary<CubeSide, Wave>();
        foreach (var cubeSide in _map.CUBESIDES.Values)
        {
            _currentWaves.Add(cubeSide, null);
        }

        _time.Value = 0f;
        _coroutineHandler.StartCoroutine(ActionLoop());
    }


    private IEnumerator ActionLoop()
    {
        while (true)
        {
            _time.Value += Time.deltaTime;
            _waveThreshold = 1;// - _waveThresholdCurve.Evaluate(_time.Value / _endGameTime.Value) * _maxWaveThreshold;

            SpawnWaves();

            yield return null;
        }
    }

    private void SpawnWaves()
    {
        foreach (var cubeSide in _map.CUBESIDES)
        {
            if (_currentWaves[cubeSide.Value] == null || _currentWaves[cubeSide.Value].WaveProgress >= _waveThreshold)
            {
                float r = Random.value;

                if (r < 0.25f)
                {
                    if (SpawnSpecialWave(cubeSide.Value))
                        continue;
                }
                else
                {
                    if (r < 0.5f)
                    {
                        _currentWaves[cubeSide.Value] = new StandardWave(this, _map, _coroutineHandler, _normalSettings.GetObstacleAmount(_time.Value), _clusterSettings.GetObstacleAmount(_time.Value), 0, 4);
                        _currentWaves[cubeSide.Value].Spawn(cubeSide.Value.Cells);
                    }
                    else
                    {
                        _currentWaves[cubeSide.Value] = new StandardWave(this, _map, _coroutineHandler, _normalSettings.GetObstacleAmount(_time.Value), 0, _spinnerSettings.GetObstacleAmount(_time.Value), 4);
                        _currentWaves[cubeSide.Value].Spawn(cubeSide.Value.Cells);
                    }
                }

            }
        }
    }

    private bool SpawnSpecialWave(CubeSide cubeSide)
    {
        float r = Random.value;

        if (_time.Value >= 75f)
        {
            if (r < 1 / 3f)
            {
                _currentWaves[cubeSide] = new MegaWave(this, _map, _coroutineHandler, 2f);
                _currentWaves[cubeSide].Spawn(cubeSide.Cells);
            }
            else
            {
                _currentWaves[cubeSide] = new RapidFire(this, _map, _coroutineHandler, _rapidFireSettings.GetObstacleAmount(_time.Value), 0.15f);
                _currentWaves[cubeSide].Spawn(cubeSide.Cells);
            }
            return true;
        }
        else
        {
            if (_rapidFireSettings.GetObstacleAmount(_time.Value) != 0)
            {
                _currentWaves[cubeSide] = new RapidFire(this, _map, _coroutineHandler, _rapidFireSettings.GetObstacleAmount(_time.Value), 0.15f);
                _currentWaves[cubeSide].Spawn(cubeSide.Cells);
                return true;
            }
        }

        return false;
    }

    public void SpawnSpinner(Cell c, Vector3 dir)
    {
        _spinnerSpawner.SpawnSpinner(c, c.Pos + c.Normal * 15, 2f, dir);
    }

    public void SpawnAsteroid(Cell c)
    {
        _asteroidSpawner.SpawnAsteroid(c, c.Pos + c.Normal * 15, 2f);
    }

    public void SpawnCluster(Cell c)
    {
        _clusterCubeSpawner.SpawnClusterCube(c, c.Pos + c.Normal * 15, 2f);
    }
}