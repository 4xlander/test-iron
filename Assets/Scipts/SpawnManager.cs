using config;
using UnityEngine;

public class SpawnManager : GenericSingletonClass<SpawnManager>
{
    //тут сделать весь контроль
    //  спаун юнитов через кнопки юай, врагов и своих и их инит
    // спаун базы ровно по центру. в координатах по Х 0
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

    private BuildingItem _baseInstance;

    private void Start()
    {
        if (_baseInstance == null)
            _baseInstance = CreateBase(baseSpawnPoint);
    }

    public void SpawnPlayerLeft() => CreatePlayerUnit(leftNearBasePoint);
    public void SpawnPlayerRight() => CreatePlayerUnit(rightNearBasePoint);
    public void SpawnEnemyLeft() => CreateEnemyUnit(leftEdgePoint);
    public void SpawnEnemyRight() => CreateEnemyUnit(rightEdgePoint);

    private UnitItem CreatePlayerUnit(Transform spawnPoint)
    {
        var unit = CreateUnit(spawnPoint);

        var dirToBase = GetDirToBase(unit.transform);
        if (dirToBase.x < 0)
            FlipUnit(unit);

        return unit;
    }

    private UnitItem CreateEnemyUnit(Transform spawnPoint)
    {
        var unit = CreateUnit(spawnPoint);
        unit.spriteRenderer.color = Color.red;

        var dirToBase = GetDirToBase(unit.transform);
        if (dirToBase.x > 0)
            FlipUnit(unit);

        return unit;
    }

    private UnitItem CreateUnit(Transform spawnPoint)
    {
        var unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
        unit.Init(unitConfigs[0]);
        return unit;
    }

    private Vector3 GetDirToBase(Transform unitTransform)
    {
        return (baseSpawnPoint.position - unitTransform.position).normalized;
    }

    private void FlipUnit(UnitItem unit)
    {
        unit.visualGO.transform.localScale = new Vector3(-1, 1, 1);
    }

    private BuildingItem CreateBase(Transform spawnPoint)
    {
        return Instantiate(basePrefab, spawnPoint.position, Quaternion.identity);
    }
}