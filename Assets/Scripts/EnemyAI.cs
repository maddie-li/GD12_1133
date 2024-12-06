using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    // REFERENCES
    private CombatManager combatManager;
    private PlayerController player;

    // NAVIGATION
    private NavMeshAgent Agent;

    [SerializeField] private GameObject PatrolPath;

    private Transform[] PatrolPoints;
    private List<Vector3> PointList = new List<Vector3>();

    private int currentPoint = 0;

    public Vector3 PlayerLocation;
    public Vector3 DirectionToPlayer;

    // STATS
    CombatantInfo enemyInfo;

    [SerializeField] string myName;
    [SerializeField] int myHealth;
    [SerializeField] List<Item> myInventory;

    public bool isDying = false;

    public void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        MakePath();
        GoToNext();

        enemyInfo = new CombatantInfo(myName, myHealth, myInventory);

    }

    public void SetManagers(CombatManager newCombatManager, PlayerController newPlayer)
    {
        combatManager = newCombatManager;
        player = newPlayer;
    }

    // MOVEMENT
    private void GoToNext()
    {
        if (PatrolPoints.Length > 0)
        {
            Agent.SetDestination(PatrolPoints[currentPoint].position);

            currentPoint = (currentPoint + 1) % PatrolPoints.Length;
        }
    }

    private void Update()
    {
        if (Agent.enabled && !Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            GoToNext();
        }

    }

    private void MakePath()
    {

        PatrolPoints = PatrolPath.GetComponentsInChildren<Transform>();

        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            PointList.Add(PatrolPoints[i].position);
        }
    }

    // SENSING
    public void CheckIfCanSeePlayer(Vector3 playerPosition)
    {

        if (!combatManager.GetInCombat())
        {

            Vector3 myPosition = transform.position;
            float playerDistance = Vector3.Distance(myPosition, playerPosition);

            DirectionToPlayer = (playerPosition - myPosition).normalized;

            RaycastHit hit;

            if (Physics.Raycast(myPosition, DirectionToPlayer, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(myPosition, DirectionToPlayer * hit.distance, Color.red);
                Debug.LogError("Ray hit: " + hit.collider.name); // Log what the ray hits

                if (combatManager != null)
                {
                    PlayerFound();
                }
                else
                {
                    Debug.Log("CombatManager is  null!");
                }

            }
        }
        
    }

    public void PlayerFound()
    {
        // MOVE AND LOOK TO PLAYER
        player.transform.LookAt(this.transform.gameObject.transform);
        transform.LookAt(player.transform.gameObject.transform);

        Agent.SetDestination(player.transform.gameObject.transform.position);

        // SET ACTIVE ENEMY
        combatManager.Enemy = enemyInfo;

        combatManager.EnemyObject = this;

        // BEGIN COMBAT
        combatManager.InitiateCombat();
        combatManager.SetInCombat(true);
    }

    // Call this when the enemy dies
    public void Die()
    {
        Debug.LogError("Enemy dead");
        isDying = true;

        // stop moving
        Agent.isStopped = true;
        Agent.enabled = false;

        Rigidbody rigidbody = this.gameObject.AddComponent<Rigidbody>();
        // fall over
        rigidbody.AddTorque(Vector3.right * 0.5f, ForceMode.Impulse);

        // destroy enemy
        Destroy(gameObject, 5f);
    }
}