using System;

public interface ISpawnerStatistics
{
    int TotalSpawned { get; }
    int TotalCreated { get; }
    int ActiveCount { get; }

    event Action StatisticsChanged;
}
