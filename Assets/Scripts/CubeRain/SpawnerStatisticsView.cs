using TMPro;
using UnityEngine;

public class SpawnerStatisticsView : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerSource;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _title;

    private ISpawnerStatistics _spawner;

    private void Awake()
    {
        _spawner = _spawnerSource as ISpawnerStatistics;
    }

    private void OnEnable()
    {
        if (_spawner == null)
            return;

        _spawner.StatisticsChanged += UpdateView;

        UpdateView();
    }

    private void OnDisable()
    {
        if (_spawner == null)
            return;

        _spawner.StatisticsChanged -= UpdateView;
    }

    private void UpdateView()
    {
        _text.text =
            $"{_title}\n" +
            $"Заспавнено за всё время: {_spawner.TotalSpawned}\n" +
            $"Создано объектов: {_spawner.TotalCreated}\n" +
            $"Активно на сцене: {_spawner.ActiveCount}";
    }
}
