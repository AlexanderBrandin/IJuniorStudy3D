using UnityEngine;

public class BombSpawner : ObjectSpawner
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private ExplosionForceApplier _explosionForceApplier;
    [SerializeField] private Transform _poolContainer;

    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private PooledObjectPool<Bomb> _pool;

    public override int TotalSpawned => _pool.TotalSpawned;
    public override int TotalCreated => _pool.TotalCreated;
    public override int ActiveCount => _pool.ActiveCount;

    private void Awake()
    {
        _pool = new PooledObjectPool<Bomb>(
            CreateBomb,
            ActivateBomb,
            DeactivateBomb,
            DestroyBomb,
            _defaultCapacity,
            _maxSize
        );
    }

    public void Spawn(Vector3 position)
    {
        Bomb bomb = _pool.Get(position, Quaternion.identity);

        float lifetime = Random.Range(_minLifetime, _maxLifetime);

        bomb.Initialize(lifetime);

        NotifyStatisticsChanged();
    }

    private Bomb CreateBomb()
    {
        Bomb bomb = Instantiate(_bombPrefab, _poolContainer);

        bomb.Exploded += HandleBombExploded;

        return bomb;
    }

    private void ActivateBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
    }

    private void DeactivateBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
        bomb.transform.SetParent(_poolContainer);
    }

    private void DestroyBomb(Bomb bomb)
    {
        Destroy(bomb.gameObject);
    }

    private void HandleBombExploded(Bomb bomb, Vector3 position)
    {
        _explosionForceApplier.Explode(
            position,
            _explosionRadius,
            _explosionForce,
            bomb.Rigidbody
        );

        _pool.Release(bomb);

        NotifyStatisticsChanged();
    }
}
