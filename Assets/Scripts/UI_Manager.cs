using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] Layouts;

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
        Debug.Log("UIManager: ActivatedHUD");
        SetLayout(MenuLayouts.HUD);
    }

    public void ActivatePause()
    {
        SetLayout(MenuLayouts.Pause);
    }





}