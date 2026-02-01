using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Legacy UI Text
using TMPro;
using UnityEngine.AI; // Use this if you are using Text Mesh Pro

public class MonsterCooler : MonoBehaviour
{
    public Transform target;
    public float observationRange = 5f;
    private NavMeshAgent navMeshAgent;

    [Space]
    public float fastSpeed = 8.0f;
    public float slowSpeed = 3.5f;

    [Space]
    public float timeRemaining = 10f; // The initial countdown time in seconds

    [Space]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // set navmeshagent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // set animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            // when timer haven't runs out yet, the monster don't recognize the player
            timeRemaining -= Time.deltaTime;

            // Chase player until into "observation" range
            if (Vector3.Distance(transform.position, target.position) > observationRange)
            {
                // Slow chase player when out of observation range
                SetSpeedSlow();
                navMeshAgent.SetDestination(target.position);
            }
            else
            {
                // Stop moving when within observation range
                navMeshAgent.SetDestination(transform.position);
            }
        }
        else
        {
            // when timer runs out, the monster recognizes the player and chase like hell
            SetSpeedFast();
            timeRemaining = 0;

            // Chase player when timer is out
            navMeshAgent.SetDestination(target.position);
        }
    }

    public void SetSpeedFast()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = fastSpeed;
        }

        // Trigger fast chasing animation
        if (animator != null)
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void SetSpeedSlow()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = slowSpeed;
        }

        // Trigger slow animation
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
        }
    }

}
