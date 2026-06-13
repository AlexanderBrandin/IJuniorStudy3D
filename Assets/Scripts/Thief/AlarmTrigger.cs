using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private Thief _thief;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief) == false)
            return;

        if (thief != _thief)
            return;

        _alarm.TurnOn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief) == false)
            return;

        if (thief != _thief)
            return;

        _alarm.TurnOff();
    }
}
