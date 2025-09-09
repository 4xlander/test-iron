using System;
using UnityEngine;

public class UnitItem : MonoBehaviour
{
    public Action<UnitItem> OnUnitDie = delegate { };

    [Header("Unit Personal Ref")]
    public GameObject visualGO;
    public AudioSource unitAudioSource;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public Rigidbody2D rb;
    public Health health;
    public ParticleSystem hitEffect;

    [Header("Firing")]
    public Transform firePoint;

    public Faction Faction { get; private set; }

    private Vector3 _lookDir = Vector3.left;
    private UnitConfig _config;
    private float _currentAmmo;
    private float _fireDelayTimer;
    private float _reloadTimer;
    private bool _initialized;

    private Transform _currentTarget;
    private float _updateTargetTimer;
    private const float UpdateTargetInterval = 0.3f;

    private void Update()
    {
        if (!_initialized) return;

        _updateTargetTimer -= Time.deltaTime;
        if (_updateTargetTimer <= 0)
        {
            if (_currentTarget == null)
            {
                _updateTargetTimer = UpdateTargetInterval;
                _currentTarget = FindTarget(_lookDir, _config.range);
            }
        }

        HandleFiring();
        HandleMovement();
    }

    public void Init(UnitConfig unitConfig, Faction faction)
    {
        unitAudioSource.PlayOneShot(unitConfig.soundSpawn);

        health.Init(unitConfig.hp);
        health.OnDie += OnDie;

        _currentAmmo = unitConfig.ammoCount;
        _config = unitConfig;

        Faction = faction;
        _initialized = true;
    }

    public void SetLookDirection(Vector3 dir)
    {
        _lookDir = dir.normalized;
        if (dir.x > 0)
            visualGO.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnDie()
    {
        unitAudioSource.PlayOneShot(_config.soundDeath);
        OnUnitDie?.Invoke(this);
        Destroy(gameObject, _config.soundDeath.length);
    }

    private Transform FindTarget(Vector2 direction, float range)
    {
        var hits = Physics2D.RaycastAll(transform.position, direction, range, ~(1 << gameObject.layer));
        foreach (var hit in hits)
            if (IsValidTarget(hit.collider))
                return hit.transform;

        return null;
    }

    private bool IsValidTarget(Collider2D hitCollider)
    {
        if (hitCollider.TryGetComponent(out UnitItem unit) && unit.Faction != Faction)
        {
            unit.OnUnitDie += HandleTargetDeath;
            return true;
        }

        return Faction != Faction.Player && hitCollider.TryGetComponent(out BuildingItem _);
    }

    private void HandleTargetDeath(UnitItem unit)
    {
        unit.OnUnitDie -= HandleTargetDeath;
        _currentTarget = null;
    }

    private void HandleFiring()
    {
        if (_reloadTimer > 0)
        {
            _reloadTimer -= Time.deltaTime;
            return;
        }

        _fireDelayTimer -= Time.deltaTime;
        if (_fireDelayTimer > 0) return;


        if (_currentAmmo <= 0)
        {
            _reloadTimer = _config.reload;
            _currentAmmo = _config.ammoCount;
            return;
        }

        if (_currentTarget == null)
            return;

        _fireDelayTimer = 1f / _config.rateFire;

        var bullet = Instantiate(_config.bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.Init(_lookDir, _config.damage, Faction);
        Destroy(bullet.gameObject, 3);

        _currentAmmo--;
    }

    private void HandleMovement()
    {
        if (CanMove())
            rb.linearVelocity = _lookDir * _config.moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    private bool CanMove()
    {
        if (Faction == Faction.Player)
            return false;

        if (_currentTarget == null)
            return true;

        var distanceToTarget = Vector2.Distance(transform.position, _currentTarget.position);
        return distanceToTarget > _config.stopDistance;
    }
}