using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public Texture image;
    [SerializeField] public string itemName;

    [SerializeField] public int maxDamage;

    [SerializeField] public bool isHealItem;

    private Roller roller = new Roller();

    public int Hit()
    {
        return roller.Roll(maxDamage);
    }
}