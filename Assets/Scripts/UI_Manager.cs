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
    private CombatManager combatManager;
    private CombatantInfo playerInfo;

    [SerializeField] private GameObject[] Layouts;

    [SerializeField] private Texture[] Reticles;
    [SerializeField] private GameObject radar;
    [SerializeField] private Image health;
    [SerializeField] private Image weaponTimerImg;
    [SerializeField] private WeaponSelection[] weaponSelections;
    [SerializeField] public CombatantInfoUI[] combatantInfos;

    public TextMeshProUGUI displayPrompt;
    public TextMeshProUGUI roomText;
    public TextMeshProUGUI radarText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI deathText;

    public RawImage reticle;
    private Quaternion radarRotation;

    public bool inCombat;
    public bool HasPickedWeapon;

    // Timer variables
    private float combatTimer = 0f;
    private float combatDuration = 2f;

    private bool isCombatTiming = false;

    private float weaponTimer = 0f;
    private float weaponDuration = 6f;

    private float weaponProgress = 0f;
    private bool isWeaponTiming = false;

    private enum MenuLayouts
    {
        Main = 0,
        HUD = 1,
        Pause = 2,
        Danger = 3,
        Weapon = 4,
        Death = 5,
    }

    void Update()
    {
        UpdateTimers();
    }

    void UpdateTimers()
    {
        if (isCombatTiming && combatTimer < combatDuration)
        {
            combatTimer += Time.unscaledDeltaTime;
            if (combatTimer >= combatDuration)
            {
                isCombatTiming = false;
                combatManager.CombatBegin();
            }
        }

        if (isWeaponTiming && weaponTimer < weaponDuration)
        {
            weaponTimer += Time.unscaledDeltaTime;

            weaponProgress = (weaponTimer / weaponDuration);
            weaponTimerImg.fillAmount = 1 - weaponProgress;

            if (weaponTimer >= weaponDuration)
            {
                EndWeaponTimer(false);
            }
        }
    }

    public void SetGameManager(GameManager newGameManager)
    {
        gameManager = newGameManager;
    }

    public void SetPlayerInfo( CombatantInfo newPlayerInfo)
    {
        playerInfo = newPlayerInfo;

        for (int i = 0; i < weaponSelections.Length; i++)
        {
            weaponSelections[i].SetReferences(playerInfo, this);
        }
    }

    public void SetCombatManager(CombatManager newcombatManager)
    {
        combatManager = newcombatManager;
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

        inCombat = false;

        SetLayout(MenuLayouts.HUD);

        weaponSelections[1].ShowWeaponSelection();


        gameManager.ResumeGame();
    }

    public void ActivatePause()
    {
        SetLayout(MenuLayouts.Pause);
        gameManager.PauseGame();
    }

    public void ActivateWeapon()
    {
        HasPickedWeapon = false;

        SetLayout(MenuLayouts.Weapon);
        weaponSelections[0].ShowWeaponSelection();
        gameManager.SlowGame();

        isWeaponTiming = true;
        weaponTimer = 0f;
    }

    public void ActivateDanger()
    {
        enemyText.text = combatManager.Enemy.CombatantName;

        SetLayout(MenuLayouts.Danger);
        gameManager.SlowGame();

        isCombatTiming = true;
        combatTimer = 0f;
    }

    public void ActivateDeath()
    {
        SetLayout(MenuLayouts.Death);
        gameManager.SlowGame();
        Debug.LogError("Game ENDED player IS DEAD!!");


    }


    // PROMPTS
    public void ActivateDoorPrompt()
    {
        displayPrompt.text = "[E] Open";
    }

    public void ActivatePickupPrompt(Item item)
    {
        displayPrompt.text = $"[E] Pick up {item.name}";
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

    // COMBAT

    public void EndWeaponTimer(bool hasPickedWeapon)
    {
        HasPickedWeapon = hasPickedWeapon;
        isWeaponTiming = false;
        weaponTimer = weaponDuration;
        combatManager.CombatEnd();
    }

}
