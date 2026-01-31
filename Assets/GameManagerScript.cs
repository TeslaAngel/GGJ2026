using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Player Data
    public GameObject playerObject;
    private Transform playerTransform;
    private PlayerScript playerScript;

    // Monster Data
    // TODO: Add Monster Data

    // Map Generation
    public float mapWidth = 100.0f;
    public float mapHeight = 100.0f;

    // creature Prefabs
    // TODO: Add Creature Prefabs

    public float winTimerLimit = 120.0f; // 2 minutes to win
    public float winTimer = 0.0f;

    public bool gameWon = false;
    public bool gameLost = false;

    // Update is called once per frame
    void Update()
    {
        // Calculate Distance between Player and Monster

        // Monster Behavior
        //  Monster observation when player is within certain range

        //  Monster accelerated cool down when player have wrong immitation

        //  Monster chase when cool down timer is up

        // Win Condition

        // Lose Condition
    }
}
