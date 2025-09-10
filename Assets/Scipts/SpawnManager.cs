using System;
using config;
using UnityEngine;

public class SpawnManager : GenericSingletonClass<SpawnManager>
{
    public event Action OnBaseDestroyed;

    [Header("Prefabs")]
    [SerializeField] private BuildingItem basePrefab;
    [SerializeField] private UnitItem unitPrefab;

    [Header("Configs")]
    [SerializeField] private BuildingConfig buildingConfig;
    [SerializeField] private UnitConfig[] unitConfigs;

    [Header("Spawn Points")]
    [SerializeField] private Transform baseSpawnPoint;
    [SerializeField] private Transform leftEdgePoint;
    [SerializeField] private Transform leftNearBasePoint;
    [SerializeField] private Transform rightNearBasePoint;
    [SerializeField] private Transform rightEdgePoint;

    [Header("Layers")]
    [SerializeField] private string playerUnitLayer;
    [SerializeField] private string enemyUnitLayer;

    private BuildingItem _baseInstance;

    private void Start()
    {
        if (_baseInstance == null)
        {
            _baseInstance = CreateBase(baseSpawnPoint);
            _baseInstance.gameObject.layer = LayerMask.NameToLayer(playerUnitLayer);
            _baseInstance.health.OnDie += OnBaseDestroyed;
        }
    }

    private void OnDestroy()
    {
        if (_baseInstance != null)
            _baseInstance.health.OnDie -= OnBaseDestroyed;
    }

    public void SpawnPlayerLeft() => CreatePlayerUnit(leftNearBasePoint);
    public void SpawnPlayerRight() => CreatePlayerUnit(rightNearBasePoint);
    public void SpawnEnemyLeft() => CreateEnemyUnit(leftEdgePoint);
    public void SpawnEnemyRight() => CreateEnemyUnit(rightEdgePoint);

    private void CreatePlayerUnit(Transform spawnPoint)
    {
        var unit = CreateUnit(spawnPoint, Faction.Player);
        unit.gameObject.layer = LayerMask.NameToLayer(playerUnitLayer);
    }

    private void CreateEnemyUnit(Transform spawnPoint)
    {
        var unit = CreateUnit(spawnPoint, Faction.Enemy);
        unit.gameObject.layer = LayerMask.NameToLayer(enemyUnitLayer);
        unit.spriteRenderer.color = Color.red;
    }

    private UnitItem CreateUnit(Transform spawnPoint, Faction faction)
    {
        var unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
        unit.rb.freezeRotation = true;

        var dirToBase = GetDirToBase(unit.transform);
        var lookDir = faction == Faction.Player ? -dirToBase : dirToBase;
        unit.SetLookDirection(lookDir);

        var unitConfig = unitConfigs[0];
        unit.Init(unitConfig, faction);
        unit.OnUnitDie += HandleUnitDeath;

        Debug.Log($"{unit.Faction} unit '{unitConfig.spawnMessageUnit}' spawned");
        return unit;
    }

    private void HandleUnitDeath(UnitItem unit)
    {
        unit.OnUnitDie -= HandleUnitDeath;
        Debug.Log($"{unit.Faction} unit died");
    }

    private Vector3 GetDirToBase(Transform unitTransform)
    {
        var dir = baseSpawnPoint.position - unitTransform.position;
        return dir.x > 0 ? Vector3.right : Vector3.left;
        ;
    }

    private BuildingItem CreateBase(Transform spawnPoint)
    {
        var baseInstance = Instantiate(basePrefab, spawnPoint.position, Quaternion.identity);
        baseInstance.Init(buildingConfig);
        return baseInstance;
    }
}