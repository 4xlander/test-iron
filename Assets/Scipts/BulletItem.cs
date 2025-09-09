using UnityEngine;

public class BulletItem : MonoBehaviour
{
    public GameObject visualGO;
    public Rigidbody2D rb;
    public float moveSpeed = 8f;

    private float _damage;
    private Faction _shooterFaction;

    public void Init(Vector3 targetDir, float damage, Faction shooter)
    {
        _damage = damage;
        _shooterFaction = shooter;
        rb.linearVelocity = targetDir.normalized * moveSpeed;

        if (targetDir.x > 0)
            visualGO.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BuildingItem building))
        {
            building.health.DoDamage(_damage);
            Destroy(gameObject);
            return;
        }

        if (!other.TryGetComponent(out UnitItem unit) || unit.Faction == _shooterFaction)
            return;

        unit.health.DoDamage(_damage);
        unit.hitEffect.Play();
        Destroy(gameObject);
    }
}