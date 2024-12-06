using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public bool inRangeOfEnemy = false;
    public EnemyAI currentEnemy = null;

    public void Update()
    {
        if (inRangeOfEnemy)
        {
            if (currentEnemy != null && currentEnemy.isDying == false)
            {
                // Debug.LogWarning("In range of enemy!");
                currentEnemy.CheckIfCanSeePlayer(transform.position);
            }
        }
    }
}
