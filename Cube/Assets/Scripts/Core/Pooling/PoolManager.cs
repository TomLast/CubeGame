using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPool> _pools;

    private void Awake()
    {
        foreach (var pool in _pools)
        {
            pool.Init();
        }
    }
}
