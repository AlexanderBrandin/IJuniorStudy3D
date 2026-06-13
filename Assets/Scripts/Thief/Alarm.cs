using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

    [SerializeField] private float _volumeChangeSpeed;

    private AudioSource _audioSource;
    private Coroutine _volumeChangingCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = MinVolume;
    }

    public void TurnOn()
    {
        StartVolumeChanging(MaxVolume);
    }

    public void TurnOff()
    {
        StartVolumeChanging(MinVolume);
    }

    private void StartVolumeChanging(float targetVolume)
    {
        if (_volumeChangingCoroutine != null)
            StopCoroutine(_volumeChangingCoroutine);

        _volumeChangingCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                targetVolume,
                _volumeChangeSpeed * Time.deltaTime
            );

            yield return null;
        }

        _volumeChangingCoroutine = null;
    }
}
