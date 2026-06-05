using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _container;
    private readonly Queue<T> _objects = new Queue<T>();

    public ObjectPool(T prefab, int initialCount, Transform container)
    {
        _prefab = prefab;
        _container = container;

        for (int i = 0; i < initialCount; i++)
            AddObject();
    }

    public T Get()
    {
        if (_objects.Count == 0)
            AddObject();

        T pooledObject = _objects.Dequeue();

        pooledObject.gameObject.SetActive(true);

        return pooledObject;
    }

    public void Release(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        pooledObject.transform.SetParent(_container);
        _objects.Enqueue(pooledObject);
    }

    private void AddObject()
    {
        T pooledObject = Object.Instantiate(_prefab, _container);

        pooledObject.gameObject.SetActive(false);
        _objects.Enqueue(pooledObject);
    }
}
