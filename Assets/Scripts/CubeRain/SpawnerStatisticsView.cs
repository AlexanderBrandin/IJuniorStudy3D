using TMPro;
using UnityEngine;

public class SpawnerStatisticsView : MonoBehaviour
{
    [SerializeField] private ObjectSpawner _spawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _title;

    private void OnEnable()
    {
        _spawner.StatisticsChanged += UpdateView;

        UpdateView();
    }

    private void OnDisable()
    {
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
