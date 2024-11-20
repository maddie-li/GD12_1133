using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private GameObject FramePrefab;

    public string[] Weapons;
    private List<GameObject> WeaponsFrames = new List<GameObject>();

    GameObject frameInstance;
    TextMeshProUGUI label;

    public void ShowWeaponSelection(bool screenTypeHUD)
    {

        Debug.Log($"Setting weapon UI for HUD: {screenTypeHUD}");

        Weapons = new string[3] { "knife", "pistol", "shotgun"};

        ClearFrames();

        transform.localPosition = SetPosition(screenTypeHUD);
        transform.localScale = SetScale(screenTypeHUD);

        int frameIndexer = 0;

        for (int i = 0; i < Weapons.Length; i++)
        {

            frameInstance = Instantiate(FramePrefab, transform);
            frameInstance.transform.localPosition = new Vector3(i * 150, 0f, 0f);

            WeaponsFrames.Add(frameInstance); // add to list

            label = frameInstance.GetComponentInChildren<TextMeshProUGUI>();

            label.text = Weapons[i];

            frameIndexer += 1;
            Debug.Log($"Instantiated Frame {i}: {label.text} at {frameInstance.transform.localPosition} from {frameInstance.transform.parent.transform.parent}!");
        }
    }

    private void ClearFrames()
    {
        foreach (GameObject frame in WeaponsFrames)
        {
            Destroy(frame);
        }
        WeaponsFrames.Clear();
    }

    private Vector3 SetPosition(bool screenTypeHUD)
    {
        if (screenTypeHUD)
        {
            float contentsoffset = Weapons.Length * -25;
            return new Vector3(contentsoffset, 0, 0);
        }
        else
        {
            float contentsoffset = Weapons.Length * -75 / 2;
            return new Vector3(contentsoffset, 0, 0);
        }
    }

    private Vector3 SetScale(bool screenTypeHUD)
    {
        if (screenTypeHUD)
        {
            return new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            return new Vector3(1, 1, 1);
        }
    }
}
