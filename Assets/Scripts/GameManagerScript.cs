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
    public GameObject monsterObject;
    private Transform monsterTransform;
    private MonsterCooler monsterScript;
    public float monsterObservationRange = 20.0f;
    // TODO: Add Monster Data

    // Map Generation
    public float mapWidth = 100.0f;
    public float mapHeight = 100.0f;
    public GameObject creaturePrefab;

    // creature Beacon Prefabs
    public GameObject[] creatureBeaconPrefabs;

    public float winTimerLimit = 180.0f; // 3-1 minutes to win
    public float winTimer = 60.0f;

    public bool gameWon = false;
    public bool gameLost = false;

    private void Start()
    {
        playerTransform = playerObject.transform;
        playerScript = playerObject.GetComponent<PlayerScript>();

        monsterTransform = monsterObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate Distance between Player and Monster
        float distancePlayerMonster = Vector3.Distance(playerObject.transform.position, monsterObject.transform.position);

        // Monster Behavior
        //  Monster observation when player is within certain range
        //  Monster accelerated cool down when player have wrong immitation
        if (distancePlayerMonster < monsterObservationRange)
        {
            // Check Player Imitation
            bool imitationCorrect = true;
            //  Compare Player Movement Data with Monster Expected Movement Data
            //  Compare Player Behavior Data with Monster Expected Behavior Data
            if (!imitationCorrect)
            {
                // Start or Accelerate Monster Cool Down
                monsterScript.timeRemaining -= Time.deltaTime * 2; // Accelerate cool down
            }
        }

        //  Monster fast chase when cool down timer is up
        if (monsterScript.timeRemaining <= 0)
        {
            // Chase Player
        }

        // Win Condition
        winTimer += Time.deltaTime;
        if (winTimer >= winTimerLimit && !gameWon)
        {
            gameWon = true;
            Debug.Log("You Win!");
            // Add additional win logic here
        }

        // Lose Condition
        if (monsterScript.timeRemaining <= 0 && !gameLost)
        {
            gameLost = true;
            Debug.Log("You Lose!");
            // Add additional lose logic here
        }
    }
}
