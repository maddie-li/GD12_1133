using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int playerHealth;

    [SerializeField] public Item[] GameItems;

    [SerializeField] public List<Item> playerInventory = new List<Item>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
