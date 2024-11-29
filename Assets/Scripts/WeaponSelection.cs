using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private GameObject FramePrefab;

    private CombatantInfo playerInfo;
    private UI_Manager uiManager;

    List<Item> weaponsInInventory = new List<Item>();
    private List<GameObject> WeaponsFrames = new List<GameObject>();

    GameObject frameInstance;
    TextMeshProUGUI label;
    RawImage icon;
    Button frame;


    public void SetReferences(CombatantInfo newplayerInfo, UI_Manager newuiManager)
    {
        playerInfo = newplayerInfo;
        uiManager = newuiManager;

    }

    public void ShowWeaponSelection()
    {
        ClearFrames();

        CheckforWeapons();

        foreach (Item w in weaponsInInventory)
        {
            frameInstance = Instantiate(FramePrefab, transform);

            WeaponsFrames.Add(frameInstance); // add to list

            // label
            label = frameInstance.GetComponentInChildren<TextMeshProUGUI>();
            label.text = w.itemName;

            // icon
            icon = frameInstance.GetComponentInChildren<RawImage>();
            icon.texture = w.image;

            // button

            frame = frameInstance.transform.Find("Frame").GetComponent<Button>();
            frame.onClick.AddListener(() => FrameSelect(w));


        }
    }

    private void CheckforWeapons()
    {
        if (playerInfo != null)
        {
            List<Item> inventory = playerInfo.Inventory;

            foreach (Item i in inventory)
            {
                weaponsInInventory.Add(i);
            }
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

    public void FrameSelect(Item weapon)
    {
        playerInfo.CurrentWeapon = weapon;
        uiManager.EndWeaponTimer(true);
    }
}