using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit", order = 1)]
public class UnitConfig : ScriptableObject
{
    public string id
    {
        get { return name; }
    }

    public AudioClip soundDeath;
    public AudioClip soundSpawn;

    public float damage;
    public int hp;
    public float moveSpeed;
    public float stopDistance;
    public float range;

    public float ammoCount;
    public float rateFire;
    public float reload;
    public BulletItem bulletPrefab;

    public string spawnMessageUnit;
}