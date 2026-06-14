using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private ExplosionForceApplier _explosionForceApplier;

    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Spawn(Vector3 position)
    {
        Bomb bomb = GetObject(position, Quaternion.identity);

        float lifetime = Random.Range(_minLifetime, _maxLifetime);

        bomb.Initialize(lifetime);
    }

    protected override Bomb CreateObject()
    {
        Bomb bomb = Instantiate(_bombPrefab, PoolContainer);

        bomb.Exploded += HandleBombExploded;

        return bomb;
    }

    private void HandleBombExploded(Bomb bomb, Vector3 position)
    {
        _explosionForceApplier.Explode(
            position,
            _explosionRadius,
            _explosionForce,
            bomb.Rigidbody
        );

        ReleaseObject(bomb);
    }
}
