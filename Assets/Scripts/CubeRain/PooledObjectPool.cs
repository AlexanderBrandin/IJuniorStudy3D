using System;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObjectPool<T> where T : Component
{
    private readonly ObjectPool<T> _pool;

    public int TotalSpawned { get; private set; }
    public int TotalCreated => _pool.CountAll;
    public int ActiveCount => _pool.CountActive;

    public PooledObjectPool(
        Func<T> createFunc,
        Action<T> onGet,
        Action<T> onRelease,
        Action<T> onDestroy,
        int defaultCapacity,
        int maxSize)
    {
        _pool = new ObjectPool<T>(
            createFunc,
            onGet,
            onRelease,
            onDestroy,
            true,
            defaultCapacity,
            maxSize
        );
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T pooledObject = _pool.Get();

        pooledObject.transform.SetPositionAndRotation(position, rotation);

        TotalSpawned++;

        return pooledObject;
    }

    public void Release(T pooledObject)
    {
        _pool.Release(pooledObject);
    }
}
