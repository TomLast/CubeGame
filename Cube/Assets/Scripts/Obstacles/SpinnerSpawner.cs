using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinnerSpawner : MonoBehaviour
{
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private Map _map;
    [SerializeField] private ObjectPool _obstaclePool;
    [SerializeField] private Material _mat;
    [SerializeField] private float _channelTime;
    [SerializeField] private float _spinSpeed;
    [SerializeField] private float _moveSpeed;

    public void SpawnSpinner(Cell cell, Vector3 startPos, float timeToReachTarget, Vector3 dir)
    {
        _coroutineHandler.StartCoroutine(SpinnerExecution(cell, dir, startPos, timeToReachTarget));
        cell.mbCell.Target();
    }

    private IEnumerator SpinnerExecution(Cell cell, Vector3 dir, Vector3 startPos, float time)
    {
        Obstacle o = _obstaclePool.Spawn().GetComponent<Obstacle>();
        o.SetMaterial(_mat);
        o.OnCollision = SpinnerHit;
        o.transform.position = startPos;
        Vector3 targetPos = cell.Pos + cell.Normal * transform.localScale.x;
        o.Move(startPos, targetPos, time);

        float t = 0f;
        while ((t += Time.deltaTime) < time) // Dann halt so
            yield return null;
        o.Rotate(Vector3.Cross(cell.Normal, dir), _spinSpeed, _channelTime + time);
        t = 0f;
        while ((t += Time.deltaTime) < _channelTime) // Dann halt so
            yield return null;

        Vector3 secondTargetPos = targetPos + dir * ((_map.CellCountPerSide - 1) * _map.CellDistance);
        o.Move(o.transform.position, secondTargetPos, time, new System.Action(o.Reset));
    }

    private void SpinnerHit()
    {
        SceneManager.LoadScene(2);

    }
}
