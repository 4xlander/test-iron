using UnityEngine;
using UnityEngine.UI;

public class SpawnManagerUi : MonoBehaviour
{
    [SerializeField] private Button spawnPlayerUnitLeftBtn;
    [SerializeField] private Button spawnPlayerUnitRightBtn;
    [SerializeField] private Button spawnEnemyUnitLeftBtn;
    [SerializeField] private Button spawnEnemyUnitRightBtn;

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnSpawnPlayerUnitLeftClick()
    {
        Debug.Log("Spawn Player Left");
    }

    private void OnSpawnPlayerUnitRightClick()
    {
        Debug.Log("Spawn Player Right");
    }

    private void OnSpawnEnemyUnitLeftClick()
    {
        Debug.Log("Spawn Enemy Left");
    }

    private void OnSpawnEnemyUnitRightClick()
    {
        Debug.Log("Spawn Enemy Right");
    }

    private void AddListeners()
    {
        spawnEnemyUnitLeftBtn.onClick.AddListener(OnSpawnEnemyUnitLeftClick);
        spawnEnemyUnitRightBtn.onClick.AddListener(OnSpawnEnemyUnitRightClick);
        spawnPlayerUnitLeftBtn.onClick.AddListener(OnSpawnPlayerUnitLeftClick);
        spawnPlayerUnitRightBtn.onClick.AddListener(OnSpawnPlayerUnitRightClick);
    }

    private void RemoveListeners()
    {
        spawnEnemyUnitLeftBtn.onClick.RemoveListener(OnSpawnEnemyUnitLeftClick);
        spawnEnemyUnitRightBtn.onClick.RemoveListener(OnSpawnEnemyUnitRightClick);
        spawnPlayerUnitLeftBtn.onClick.RemoveListener(OnSpawnPlayerUnitLeftClick);
        spawnPlayerUnitRightBtn.onClick.RemoveListener(OnSpawnPlayerUnitRightClick);
    }
}