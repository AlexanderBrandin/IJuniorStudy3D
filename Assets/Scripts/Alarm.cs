using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

    [SerializeField] private float _volumeChangeSpeed;

    private AudioSource _audioSource;
    private float _targetVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = MinVolume;
        _targetVolume = MinVolume;
    }

    private void Update()
    {
        ChangeVolume();
    }

    public void TurnOn()
    {
        _targetVolume = MaxVolume;
    }

    public void TurnOff()
    {
        _targetVolume = MinVolume;
    }

    private void ChangeVolume()
    {
        _audioSource.volume = Mathf.MoveTowards(
            _audioSource.volume,
            _targetVolume,
            _volumeChangeSpeed * Time.deltaTime
        );
    }
}
