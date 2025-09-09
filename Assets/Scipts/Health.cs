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

    public void DoDamage(int damage)
    {
        
    }
}
