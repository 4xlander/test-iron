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

    public void Init(UnitConfig unitConfig)
    {
        // тут должен быть загружен конфиг
    }
    
    public void OnDie()
    {
        // Тут юнит умирает и себя деспаунит
    }
}