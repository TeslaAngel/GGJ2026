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
    private float monsterObservationRange;

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
        monsterScript = monsterObject.GetComponent<MonsterCooler>();
        monsterScript.target = playerTransform;
        monsterObservationRange = monsterScript.observationRange;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate Distance between Player and Monster
        float distancePlayerMonster = Vector3.Distance(playerObject.transform.position, monsterObject.transform.position);

        // Monster Behavior
        //  (done locally) Monster observation when player is within certain range
        //  (done locally) Monster accelerated cool down when player have wrong immitation

        // If player made a mistake, accelerate monster cool down
        if (distancePlayerMonster < monsterObservationRange)
        {
            // TODO Check Player Imitation
            bool imitationCorrect = true;
            //  TODO Compare Player Movement Data with Monster Expected Movement Data
            //  TODO Compare Player Behavior Data with Monster Expected Behavior Data
            if (!imitationCorrect)
            {
                // Start or Accelerate Monster Cool Down
                monsterScript.timeRemaining -= Time.deltaTime * 2; // Accelerate cool down
            }
        }

        // Win Condition
        winTimer += Time.deltaTime;
        if (winTimer >= winTimerLimit && !gameWon)
        {
            gameWon = true;
            Debug.Log("You Win!");
            // TODO Add additional win logic here
        }

        // Lose Condition
        if (monsterScript.timeRemaining <= 0 && !gameLost)
        {
            gameLost = true;
            Debug.Log("You Lose!");
            // TODO Add additional lose logic here
        }
    }
}
