using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool")]
public class ObjectPool : ScriptableObject
{
    public GameObject _pooledObject;
    public int _poolSize;
    public Queue<GameObject> _pool = new Queue<GameObject>();
    private Transform _parent;

    public void Init()
    {
        _pool.Clear();
        _parent = new GameObject($"{_pooledObject.name} Pool").transform;

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject go = Instantiate(_pooledObject, _parent);
            _pool.Enqueue(go);
            go.SetActive(false);
        }
    }

    public GameObject Spawn()
    {
        GameObject go = GetObject();
        go.SetActive(true);
        return go;
    }
    public void Despawn(GameObject o)
    {
        if (_pool.Count < _poolSize)
        {
            if (!_pool.Contains(o))
            {
                _pool.Enqueue(o);
            }
            o.SetActive(false);
        }
        else
            Destroy(o);
    }

    protected GameObject GetObject()
    {
        GameObject go;
        if (_pool.Count <= 0)
            go = Instantiate(_pooledObject, _parent);
        else
        {
            go = _pool.Dequeue();
        }

        return go;
    }
}
