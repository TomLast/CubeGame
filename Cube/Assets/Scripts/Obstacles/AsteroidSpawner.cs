using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private ObjectPool _obstaclePool;
    [SerializeField] private float _minRoationSpeed;
    [SerializeField] private float _maxRoationSpeed;
    [SerializeField] private Material _mat;

    public void SpawnAsteroid(Cell cell, Vector3 startPos, float timeToReachTarget)
    {
        Obstacle o = _obstaclePool.Spawn().GetComponent<Obstacle>();
        o.SetMaterial(_mat);
        o.OnCollision = AsteroidHit;
        o.transform.position = startPos;
        o.Move(startPos, cell.Pos, timeToReachTarget, new System.Action(o.Reset));
        o.Rotate(new Vector3(Random.value, Random.value, Random.value).normalized, Random.Range(_minRoationSpeed, _maxRoationSpeed), timeToReachTarget);
        cell.mbCell.Target();
    }

    private void AsteroidHit()
    {
        SceneManager.LoadScene(2);

    }
}
