using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Player Data
    public GameObject playerObject;
    private Transform playerTransform;
    private PlayerScript playerScript;

    [Space]
    // Monster Data
    public GameObject monsterObject;
    private Transform monsterTransform;
    private MonsterCooler monsterScript;
    private float monsterObservationRange;
    public float coolDownAccelerationFactor = 2.0f;
    private float monsterCoolDownTimerLimit;

    [Space]
    // Map Generation
    public float mapWidth = 100.0f;
    public float mapHeight = 100.0f;
    //public GameObject creaturePrefab;

    // creature Beacon Prefabs
    public int numberOfCreatureBeacons = 10;
    public float minDistanceBetweenBeacons = 5f;
    public GameObject[] creatureBeaconPrefabs;
    private List<GameObject> activeCreatureBeacons = new List<GameObject>();

    [Space]
    public float winTimerLimit = 120.0f; // 2-1 minutes to win
    public float winTimer = 60.0f;

    [Space]
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
        monsterCoolDownTimerLimit = monsterScript.timeRemaining;

        // Generate Creature Beacons at random positions
        int maxAttemptsPerBeacon = 50;   // prevents infinite loops

        List<Vector3> placedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfCreatureBeacons; i++)
        {
            Vector3 candidatePos = Vector3.zero;
            bool validPosition = false;

            for (int attempt = 0; attempt < maxAttemptsPerBeacon; attempt++)
            {
                float randomX = Random.Range(-mapWidth / 2f, mapWidth / 2f);
                float randomY = Random.Range(-mapHeight / 2f, mapHeight / 2f);
                candidatePos = new Vector3(randomX, randomY, 0f);

                // Check distance from all existing beacons
                bool tooClose = false;
                foreach (var pos in placedPositions)
                {
                    if (Vector3.Distance(candidatePos, pos) < minDistanceBetweenBeacons)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    validPosition = true;
                    break;
                }
            }

            // Only place if we found a valid spot
            if (validPosition)
            {
                int prefabIndex = Random.Range(0, creatureBeaconPrefabs.Length);
                GameObject beaconPrefab = creatureBeaconPrefabs[prefabIndex];

                GameObject beaconInstance =
                    Instantiate(beaconPrefab, candidatePos, Quaternion.identity);

                activeCreatureBeacons.Add(beaconInstance);
                placedPositions.Add(candidatePos);
            }
            else
            {
                Debug.LogWarning("Could not find valid position for beacon after many attempts.");
            }
        }
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
        bool imitationCorrect = false;
        // Check Player Imitation with every beacon in range
        for (int i = 0; i < activeCreatureBeacons.Count; i++)
        {
            bool movementImitationCorrect = false;
            bool behaviorImitationCorrect = false;

            float distancePlayerBeacon = Vector3.Distance(playerObject.transform.position, activeCreatureBeacons[i].transform.position);
            CreatureBeaconScript beaconScript = activeCreatureBeacons[i].GetComponent<CreatureBeaconScript>();

            if (distancePlayerMonster <= monsterObservationRange)
            {
                if (distancePlayerBeacon <= beaconScript.range && playerScript.maskIndex == beaconScript.maskIndex)
                {
                    //DEBUG
                    //Debug.Log("Player within range of beacon " + i);

                    //  Compare Player Movement Data with Creature Beacon Expected Movement Data
                    //      if beacon don't require movement data, consider it correct
                    if (beaconScript.movementData.Count == 0)
                    {
                        movementImitationCorrect = true;
                    }
                    else
                    if (playerScript.movementData.Count > 0 && beaconScript.movementData.Count > 0)
                    {
                        if (playerScript.movementData.SequenceEqual(beaconScript.movementData))
                        {
                            movementImitationCorrect = true;
                        }
                    }

                    //  Compare Player Behavior Data with Monster Expected Behavior Data
                    //      if beacon don't require behavior data, consider it correct
                    if (beaconScript.behaviorData.Count == 0)
                    {
                        behaviorImitationCorrect = true;
                    }
                    else
                    if (playerScript.behaviorData.Count > 0 && beaconScript.behaviorData.Count > 0)
                    {
                        //Debug.Log("Comparing Player Behavior with Beacon Behavior");
                        if (playerScript.behaviorData.SequenceEqual(beaconScript.behaviorData))
                        {
                            behaviorImitationCorrect = true;
                        }
                    }
                }

                if (movementImitationCorrect && behaviorImitationCorrect && beaconScript.activeDuration > 0)
                {
                    imitationCorrect = true;
                    beaconScript.activeDuration -= Time.deltaTime; // decrease active duration
                    break; // no need to check other beacons
                }
            }
        }

        if (!imitationCorrect)
        {
            // Start or Accelerate Monster Cool Down
            monsterScript.timeRemaining -= Time.deltaTime * coolDownAccelerationFactor; // Accelerate cool down
        }
        // if imitation correct, reset monster cooldown timer
        else
        {
            Debug.Log("Imitation Correct");
            // reset monster time cooldown #SUBJECT TO CHANGE
            monsterScript.timeRemaining = monsterCoolDownTimerLimit;
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
        //  condition 1: if you get caught by the monster (handled in MonsterCooler.cs)
        if (monsterScript.timeRemaining <= 0 && !gameLost)
        {
            if(distancePlayerMonster <= 2f) // caught distance threshold
            {
                gameLost = true;
                Debug.Log("You Lose!");
                // TODO Add additional lose logic here

                FindObjectOfType<CameraScript>().Shake(0.3f, 0.5f);
            }
        }


    }
}
