using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject mesh;
    [SerializeField] Texture image;
    [SerializeField] string itemname;
/*
    void Start()
    {
        gameMap = Instantiate(GameMapPrefab);
        gameMap.transform.position = Vector3.zero;
    }*/

}

public class Weapon : Item
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}