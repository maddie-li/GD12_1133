using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public Texture image;
    [SerializeField] public string itemName;

    [SerializeField] int maxDamage;
    private float dealtDamage;

    public void Use()
    {

    }


    public void Hit()
    {

    }
}