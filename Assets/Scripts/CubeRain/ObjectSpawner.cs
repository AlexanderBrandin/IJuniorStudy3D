using System;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    public event Action StatisticsChanged;

    public abstract int TotalSpawned { get; }
    public abstract int TotalCreated { get; }
    public abstract int ActiveCount { get; }

    protected void NotifyStatisticsChanged()
    {
        StatisticsChanged?.Invoke();
    }
}
