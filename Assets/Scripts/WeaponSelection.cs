using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private GameObject FramePrefab;
    [SerializeField] private Item[] GameWeapons;

    [SerializeField] private PlayerInfo playerInfo;

    List<Item> weaponsInInventory = new List<Item>();
    private List<GameObject> WeaponsFrames = new List<GameObject>();

    GameObject frameInstance;
    TextMeshProUGUI label;
    RawImage icon;

    public void ShowWeaponSelection(bool screenTypeHUD)
    {
        ClearFrames();

        CheckforWeapons();

        int frameIndexer = 0;

        foreach (Item w in weaponsInInventory)
        {
            frameInstance = Instantiate(FramePrefab, transform);

            WeaponsFrames.Add(frameInstance); // add to list

            // label
            label = frameInstance.GetComponentInChildren<TextMeshProUGUI>();
            label.text = w.itemName;/*

            // make it readable when in HUD
            if (screenTypeHUD)
            {
                label.transform.localScale = new Vector3(2, 2, 2);
            }*/

            // icon
            icon = frameInstance.GetComponentInChildren<RawImage>();
            icon.texture = w.image;
            
        }
    }

    private void CheckforWeapons()
    {
        List<Item> inventory = playerInfo.playerInventory;

        foreach (Item i in inventory)
        {
            weaponsInInventory.Add(i);
        }
    }

    private void ClearFrames()
    {
        foreach (GameObject frame in WeaponsFrames)
        {
            Destroy(frame);
        }
        WeaponsFrames.Clear();

        weaponsInInventory.Clear();
    }
}