using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantInfo
{
    public string CombatantName;
    public float Health;
    public float MaxHealth;

    [SerializeField] public List<Item> Inventory = new List<Item>();

    public Item CurrentWeapon;

    // CONSTRUCTOR
    public CombatantInfo(string combatantName, float health, List<Item> inventory)
    {
        this.Health = health;
        this.MaxHealth = health;
        this.Inventory = inventory;
        this.CombatantName = combatantName;

    }
}
