using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, ISpawnerStatistics where T : Component
{
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<T> _pool;
    private int _totalSpawned;

    public event Action StatisticsChanged;

    public int TotalSpawned => _totalSpawned;
    public int TotalCreated => GetPool().CountAll;
    public int ActiveCount => GetPool().CountActive;

    protected Transform PoolContainer => _poolContainer;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected T GetObject(Vector3 position, Quaternion rotation)
    {
        T pooledObject = GetPool().Get();

        pooledObject.transform.SetPositionAndRotation(position, rotation);
        _totalSpawned++;

        NotifyStatisticsChanged();

        return pooledObject;
    }

    protected void ReleaseObject(T pooledObject)
    {
        GetPool().Release(pooledObject);

        NotifyStatisticsChanged();
    }

    protected abstract T CreateObject();

    private ObjectPool<T> GetPool()
    {
        if (_pool == null)
            InitializePool();

        return _pool;
    }

    private void InitializePool()
    {
        if (_pool != null)
            return;

        _pool = new ObjectPool<T>(
            CreateObject,
            ActivateObject,
            DeactivateObject,
            DestroyObject,
            true,
            _defaultCapacity,
            _maxSize
        );
    }

    protected void NotifyStatisticsChanged()
    {
        StatisticsChanged?.Invoke();
    }

    protected virtual void ActivateObject(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    protected virtual void DeactivateObject(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        pooledObject.transform.SetParent(_poolContainer);
    }

    protected virtual void DestroyObject(T pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
