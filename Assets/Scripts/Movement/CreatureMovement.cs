using System.Collections;               // Required for Coroutines
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;                   // Required for NavMeshAgent

public class CreatureMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // The radius within which the enemy will wander
    public float wanderTimer = 5f; // The time between new destination calculations
    //public float chaseDistance = 8f; // Distance at which enemy starts chasing the player
    public float enemyWanderSpeed = 3f; // Speed when wandering
    //public float enemyChaseSpeed = 5f; // Speed when chasing

    private NavMeshAgent agent;
    private float timer;
    //private Transform playerTarget;
    private Vector3 startPosition;
    //private bool isChasingPlayer = false;

    void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        //playerTarget = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player by tag
        startPosition = transform.position;             // Store the initial position
        timer = wanderTimer;                            // Initialize the timer
    }

    void Update()
    {
        timer += Time.deltaTime;
        // creature wandering logic
        if (timer >= wanderTimer)
             {
                 WanderToNewDestination();
                 timer = 0;
             }
        
        /*
        if (playerTarget != null)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceFromPlayer <= chaseDistance)
            {
                isChasingPlayer = true;
            }
            // Optional: add a distance to stop chasing if player runs very far away
            // if (distanceFromPlayer > chaseDistance + 30f) { isChasingPlayer = false; }
        }

        if (isChasingPlayer)
        {
            agent.speed = enemyChaseSpeed;
            agent.SetDestination(playerTarget.position);
        }
        else // Wandering logic
        {
            if (timer >= wanderTimer)
            {
                WanderToNewDestination();
                timer = 0;
            }
        }*/
    }

    private void WanderToNewDestination()
    {
        agent.speed = enemyWanderSpeed;

        // Pick a random point within the wander radius around the starting position
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += startPosition;       // Constrain roaming to the initial area
        NavMeshHit hit;

        // Find the nearest point on the NavMesh to the randomly chosen location
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    // Optional: Draw the wander radius in the editor for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
       // Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
