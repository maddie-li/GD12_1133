using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private GameManager gameManager;
    private UI_Manager uiManager;

    public CombatantInfo Player;
    public CombatantInfo Enemy;

    public CombatantInfoUI PlayerInfo;
    public CombatantInfoUI EnemyInfo;

    public EnemyAI EnemyObject;

    private Roller roller = new Roller();

    private int turns;

    private int playerDamage;
    private int enemyDamage;

    internal void SetManagers(GameManager newGameManager, UI_Manager newUIManager, CombatantInfo newPlayerInfo)
    {
        gameManager = newGameManager;
        uiManager = newUIManager;
        Player = newPlayerInfo;
    }

    public void InitiateCombat()
    {
        uiManager.ActivateDanger();
        Player.Health = Player.MaxHealth;
        Debug.LogWarning(Enemy.CombatantName);

        // turns = 0;
    }

    public void CombatBegin()
    {
        turns = 0;

        uiManager.ActivateCombat();
    }

    public void CombatEnd()
    {
        Debug.LogError(uiManager.HasPickedWeapon);

        if (uiManager.HasPickedWeapon == false || Player.CurrentWeapon == null)
        {
            Debug.LogError("No weapon selected");
            uiManager.deathText.text = "you weren't fast enough";
            EndCombat(Enemy);
        }
        else
        {
            GetDamage();
        }

        if (Player.Health > 0 && Enemy.Health > 0 && uiManager.HasPickedWeapon)
        {
            CombatBegin();
        }
    }

    public void GetDamage()
    {
        turns += 1;

        Debug.LogWarning(turns);
        Debug.LogWarning(turns);

        // Randomize enemy weapon
        int _enemyWeaponIndex = roller.Roll(Enemy.Inventory.Count());
        Enemy.CurrentWeapon = Enemy.Inventory[_enemyWeaponIndex-1];

        // Use enemy weapon
        enemyDamage = Enemy.CurrentWeapon.Hit();

        // Use player weapon
        playerDamage = Player.CurrentWeapon.Hit();

        // Display
        Debug.LogWarning($"Player used {Player.CurrentWeapon} (max {Player.CurrentWeapon.maxDamage}) for {playerDamage} damage");
        Debug.LogWarning($"Enemy used {Enemy.CurrentWeapon} (max {Enemy.CurrentWeapon.maxDamage}) for {enemyDamage} damage");

        CombatOutcome();

        if (Enemy.Health <= 0)
        {
            EndCombat(Player);
        }
        else if (Player.Health <= 0)
        {
            uiManager.deathText.text = "you were defeated";
            EndCombat(Enemy);
        }
    }

    public void CombatOutcome()
    {
        if (Player.CurrentWeapon.isHealItem)
        {
            DoHeal();
        }
        else
        {
            DoDamage();
        }

        Player.Health = Mathf.Clamp(Player.Health, 0, Player.MaxHealth);
    }

    void DoHeal()
    {
        Player.Health += playerDamage;
        Player.Health -= enemyDamage;

        DisplayDamage(playerDamage - enemyDamage, 0);
    }

    void DoDamage()
    {
        Enemy.Health -= playerDamage;
        Player.Health -= enemyDamage;

        DisplayDamage(-enemyDamage, -playerDamage);
    }

    void DisplayDamage(int playerNetDamage, int enemyDamage)
    {
        Debug.LogError(turns);


        // display player damage
        if (playerNetDamage > 0)
        {
            PlayerInfo.damageTaken.color = Color.green;
            PlayerInfo.damageTaken.text = $"+ {playerNetDamage}";
        }
        else if (playerNetDamage == 0)
        {
            PlayerInfo.damageTaken.color = Color.white;
            PlayerInfo.damageTaken.text = $"+ {playerNetDamage+enemyDamage} - {enemyDamage}";
        }
        else
        {
            PlayerInfo.damageTaken.color = Color.red;
            PlayerInfo.damageTaken.text = $"{playerNetDamage}";
        }

        // display enemy damage
        EnemyInfo.damageTaken.color = Color.red;
        EnemyInfo.damageTaken.text = $"{enemyDamage}";

        // clear
        if (turns == 0)
        {
            PlayerInfo.damageTaken.text = "";
            EnemyInfo.damageTaken.text = "";
        }
        if (enemyDamage == 0)
        {
            EnemyInfo.damageTaken.text = "";
        }

    }

    /*
        public void DealDamage()
        {
            PlayerInfo.damageTaken.gameObject.SetActive(true);
            EnemyInfo.damageTaken.gameObject.SetActive(true);

            if (Player.CurrentWeapon.isHealItem)
            {
                Player.Health += playerDamage;
                Player.Health -= enemyDamage;

                if (turns > 0)
                {
                    int netDamage = playerDamage - enemyDamage;

                    if (netDamage > 0)
                    {
                        PlayerInfo.damageTaken.color = Color.green;
                        PlayerInfo.damageTaken.text = $"+ {netDamage}";
                    }
                    else if (netDamage == 0)
                    {
                        PlayerInfo.damageTaken.color = Color.white;
                        PlayerInfo.damageTaken.text = $"+ 0";
                    }
                    else
                    {
                        PlayerInfo.damageTaken.color = Color.red;
                        PlayerInfo.damageTaken.text = $"{netDamage}";
                    }

                    EnemyInfo.damageTaken.color = Color.white;
                    EnemyInfo.damageTaken.text = "- 0";

                }

            }
            else
            {
                Enemy.Health -= playerDamage;
                Player.Health -= enemyDamage;

                if (turns > 0)
                {

                    PlayerInfo.damageTaken.color = Color.red;
                    PlayerInfo.damageTaken.text = $"- {enemyDamage}";

                    EnemyInfo.damageTaken.color = Color.red;
                    EnemyInfo.damageTaken.text = $"- {playerDamage}";
                }
                else
                {
                    PlayerInfo.damageTaken.gameObject.SetActive(false);
                    EnemyInfo.damageTaken.gameObject.SetActive(false);
                }
            }
        }*/

    public bool GetInCombat()
    {
        return uiManager.inCombat;
    }

    public void SetInCombat(bool state)
    {
        uiManager.inCombat = state;
    }

    public void Update()
    {
        uiManager.combatantInfos[0].combatant = Player;
        uiManager.combatantInfos[1].combatant = Enemy;

        PlayerInfo = uiManager.combatantInfos[0];
        EnemyInfo = uiManager.combatantInfos[1];
    }

    public void EndCombat(CombatantInfo winner)
    {

        if (winner == Player)
        {
            uiManager.objectiveText.text = "";

            Player.MaxHealth += 5;

            uiManager.objectiveText.text += $"Health increased to {Player.MaxHealth}";

            DropWeapon();



            EnemyObject.Die();

            gameManager.enemyCount -= 1;
            Debug.Log($"DEFEATED enemy now there is {gameManager.enemyCount} left");

            uiManager.ActivateHUD();
        }
        else
        {
            gameManager.LoseGame();
        }
    }

    public void DropWeapon()
    {
        Debug.Log("Dropping weapon");
        foreach (Item w in Enemy.Inventory)
        {
            if (!Player.Inventory.Contains(w))
            {
                Player.Inventory.Add(w);
                Debug.Log($"Giving player {w}");

                uiManager.objectiveText.text += $"\nPicked up {Enemy.CombatantName}'s {w.itemName}";
            }
        }

        
    }
}
