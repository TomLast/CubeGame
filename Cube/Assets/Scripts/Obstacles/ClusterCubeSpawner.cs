using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClusterCubeSpawner : MonoBehaviour
{
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private ObjectPool _obstaclePool;
    [SerializeField] private Material _mat;
    [SerializeField] private Map _map;
    [SerializeField] private AnimationCurveVariable _offset;
    [SerializeField] private int _ringCount;
    [SerializeField] private float _probability;
    [SerializeField] private float _minRoationSpeed;
    [SerializeField] private float _maxRoationSpeed;


    public void SpawnClusterCube(Cell cell, Vector3 startPos, float timeToReachTarget)
    {
        _coroutineHandler.StartCoroutine(ClusterCubeImpact(cell, startPos, timeToReachTarget));
        cell.mbCell.Target();
    }

    private IEnumerator ClusterCubeImpact(Cell cell, Vector3 startPos, float timeToReachTarget)
    {
        Obstacle o = _obstaclePool.Spawn().GetComponent<Obstacle>();
        o.SetMaterial(_mat);
        o.OnCollision = ClusterCubeHit;
        o.transform.position = startPos;
        o.Rotate(new Vector3(Random.value, Random.value, Random.value).normalized, Random.Range(_minRoationSpeed, _maxRoationSpeed), timeToReachTarget);
        o.Move(startPos, cell.Pos, timeToReachTarget, new System.Action(o.Reset));

        float t = 0f;
        while ((t += Time.deltaTime) < timeToReachTarget) // Dann halt so
            yield return null;

        Vector3 cellPos = cell.Pos;
        Vector3 cellForward = cell.mbCell.transform.forward;
        Vector3 cellRight = cell.mbCell.transform.right;
        Vector3 cellNormal = cell.Normal;
        Vector3 start = cellPos - _ringCount * cellForward * _map.CellDistance - _ringCount * cellRight * _map.CellDistance;

        for (int y = 0; y < _ringCount * 2 + 1; y++)
        {
            for (int x = 0; x < _ringCount * 2 + 1; x++)
            {
                if (y == _ringCount && x == _ringCount)
                    continue;

                float r = Random.value;

                if (r <= _probability)
                {
                    Vector3 pos = start + (x * cellRight * _map.CellDistance) + (y * cellForward * _map.CellDistance);

                    if (!_map.CheckBoundary(pos, cellNormal))
                        continue;

                    o = _obstaclePool.Spawn().GetComponent<Obstacle>();
                    o.SetMaterial(_mat);
                    o.OnCollision = ClusterCubeHit;
                    o.transform.position = cellPos;
                    o.Rotate(new Vector3(Random.value, Random.value, Random.value).normalized, Random.Range(_minRoationSpeed, _maxRoationSpeed), timeToReachTarget);
                    o.Move(cellPos, pos, timeToReachTarget, new System.Action(o.Reset), _offset.Value, cellNormal, 3f);
                }
            }
        }
    }

    private void ClusterCubeHit()
    {
        SceneManager.LoadScene(2);

    }
}
