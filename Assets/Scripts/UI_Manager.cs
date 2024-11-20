using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UI_Manager : MonoBehaviour
{

    private GameManager gameManager;
    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private GameObject[] Layouts;

    [SerializeField] private Texture[] Reticles;
    [SerializeField] private GameObject radar;
    [SerializeField] private Image health;
    [SerializeField] private WeaponSelection[] weaponSelections;

    public TextMeshProUGUI displayPrompt;
    public RawImage reticle;
    private Quaternion radarRotation;

    private enum MenuLayouts
    {
        Main = 0,
        HUD = 1,
        Pause = 2,
        Combat = 3
    }

    internal void SetManager(GameManager newgameManager)
    {
        gameManager = newgameManager;
    }

    public void OnStartButton()
    {
        Debug.Log("Start Button");
        Debug.Log(gameManager);
        gameManager.OnStartButton();
    }

    public void OnResumeButton()
    {
        Debug.Log("Resume Button");
        gameManager.ResumeGame();

        ActivateHUD();
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit Button");
        gameManager.ExitGame();
    }

    // LAYOUTS
    private void SetLayout(MenuLayouts layout)
    {
        for (int i = 0; i < Layouts.Length; i++)
        {
            Layouts[i].SetActive((int)layout == i);
        }
        Debug.Log($"Setting layout {layout}");
    }
     
    public void ActivateMainMenu()
    {
        SetLayout(MenuLayouts.Main);
    }

    public void ActivateHUD()
    {
        SetLayout(MenuLayouts.HUD);
        weaponSelections[1].ShowWeaponSelection(true);
       
        gameManager.ResumeGame();
    }

    public void ActivatePause()
    {
        SetLayout(MenuLayouts.Pause);
        gameManager.PauseGame();
    }

    public void ActivateCombat()
    {
        SetLayout(MenuLayouts.Combat);
        weaponSelections[0].ShowWeaponSelection(false);
        gameManager.SlowGame();
    }

    // PROMPTS
    public void ActivateDoorPrompt()
    {
        displayPrompt.text = "[E] Open";
    }

    public void DeactivatePrompt()
    {
        displayPrompt.text = "";
    }

    // RETICLE

    public void SetReticle(int i)
    {
        reticle.texture = Reticles[i];
    }

    public void UpdateRadar(float playerRotation)
    {
        radarRotation = Quaternion.Euler(0f, 0f, playerRotation);
        radar.transform.rotation = radarRotation;
    }

    // HEALTH

    private void Update()
    {
        float fillAmount = ((float) playerInfo.playerHealth / (float)100);

        health.fillAmount = fillAmount;

    }

}
