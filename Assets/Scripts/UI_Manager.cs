using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] Layouts;

    public TextMeshProUGUI displayPrompt;

    private enum MenuLayouts
    {
        Main = 0,
        HUD = 1,
        Pause = 2
    }

    private void Start()
    {
        ActivateMainMenu();
    }

    private void SetLayout(MenuLayouts layout)
    {
        for (int i = 0; i < Layouts.Length; i++)
        {
            Layouts[i].SetActive((int)layout == i);
        }
    }

    public void ActivateMainMenu()
    {
        SetLayout(MenuLayouts.Main);
    }

    public void ActivateHUD()
    {
        SetLayout(MenuLayouts.HUD);
    }

    public void ActivatePause()
    {
        SetLayout(MenuLayouts.Pause);
    }
    public void ActivateDoorPrompt()
    {
        displayPrompt.text = "[E] Open";
    }

    public void DeactivatePrompt()
    {
        displayPrompt.text = "";
    }





}
