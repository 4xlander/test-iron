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

    public float Damage { get; private set; }
    public float MoveSpeed { get; private set; }
    public float StopDistance { get; private set; }
    public float Range { get; private set; }

    public float AmmoCount { get; private set; }
    public float RateFire { get; private set; }
    public float Reload { get; private set; }

    public void Init(UnitConfig unitConfig)
    {
        Damage = unitConfig.damage;
        health.Init(unitConfig.hp);
        MoveSpeed = unitConfig.moveSpeed;
        StopDistance = unitConfig.stopDistance;
        Range = unitConfig.range;

        AmmoCount = unitConfig.ammoCount;
        RateFire = unitConfig.rateFire;
        Reload = unitConfig.reload;
    }

    public void OnDie()
    {
        // Тут юнит умирает и себя деспаунит
    }
}