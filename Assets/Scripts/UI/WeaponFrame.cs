using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponFrame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private RawImage icon;
    [SerializeField] private Button frame;

    private Item item;
    private WeaponSelection weaponSelection;

    public void Setup(Item _item, WeaponSelection _weaponSelection)
    {
        icon.texture = _item.image;
        label.text = _item.itemName;

        item = _item;
        weaponSelection = _weaponSelection;
    }


    public void SendSelection()
    {
        weaponSelection.FrameSelect(item);
    }
}
