using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatantInfoUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI combatantName;
    [SerializeField] public TextMeshProUGUI combatantInventory;
    [SerializeField] public TextMeshProUGUI damageTaken;
    [SerializeField] private Image combatantHealth;

    [SerializeField] public GameObject WeaponFrame;
    private TextMeshProUGUI weaponFrameText;
    private RawImage weaponFrameIcon;

    public CombatantInfo combatant = null;

    public void Update()
    {
        if (combatant != null)
        {
            // name and health
            combatantName.text = combatant.CombatantName + $"   {combatant.Health} / {combatant.MaxHealth} hp";
            combatantInventory.text = SetInventory(combatant);

            // health bar
            float fillAmount = (combatant.Health / combatant.MaxHealth);
            combatantHealth.fillAmount = fillAmount;

            if (combatant.CurrentWeapon != null)
            {
                WeaponFrame.SetActive(true);

                // label
                weaponFrameText = WeaponFrame.GetComponentInChildren<TextMeshProUGUI>();
                weaponFrameText.text = combatant.CurrentWeapon.itemName;

                // icon
                weaponFrameIcon = WeaponFrame.GetComponentInChildren<RawImage>();
                weaponFrameIcon.texture = combatant.CurrentWeapon.image;
            }
            else
            {
                WeaponFrame.SetActive(false);
            }
        }
    }


    public string SetInventory(CombatantInfo cmbt)
    {
        string _InventoryText;
        List<string> _Inventory = new List<string>();

        foreach (Item item in cmbt.Inventory)
        {

            string _itemInfo = $"{item.itemName}: {item.maxDamage} dmg";
            _Inventory.Add(_itemInfo);
        }

        _InventoryText = string.Join("\n", _Inventory);

        return _InventoryText;

    }
}
