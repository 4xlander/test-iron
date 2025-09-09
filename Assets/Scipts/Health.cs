using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnHPChange = delegate { };
    public Action OnDie = delegate { };

    public float HP { get; private set; }
    public int MaxHP { get; private set; }

    public void Init(int maxHP)
    {
        HP = maxHP;
        MaxHP = maxHP;
    }

    public void DoDamage(float damage)
    {
        if (HP <= 0) return;

        HP = Mathf.Max(0, HP - Mathf.Max(0, damage));
        OnHPChange?.Invoke();

        if (HP <= 0)
            OnDie?.Invoke();
    }
}