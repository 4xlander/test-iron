using config;
using UnityEngine;

public class BuildingItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer hpBarSprite;
    public Collider2D collider;
    public Health health;

    public void Init(BuildingConfig config)
    {
        health.Init(config.maxHp);
        health.OnHPChange += OnHPChanged;
        OnHPChanged();
    }

    private void OnHPChanged()
    {
        var hpNormalized = health.HP / health.MaxHP;
        hpBarSprite.transform.localScale = new Vector3(hpNormalized, 1f, 1f);
    }
}
