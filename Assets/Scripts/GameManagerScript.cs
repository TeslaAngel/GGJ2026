using System.Collections;
using System.Collections.Generic;
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

    [Space]
    // Map Generation
    public float mapWidth = 100.0f;
    public float mapHeight = 100.0f;
    public GameObject creaturePrefab;

    // creature Beacon Prefabs
    public GameObject[] creatureBeaconPrefabs;
    private List<GameObject> activeCreatureBeacons = new List<GameObject>();

    [Space]
    public float winTimerLimit = 180.0f; // 3-1 minutes to win
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

        // Generate Creature Beacons at random positions
        int numberOfBeacons = 10;
        float minDistance = 10f;
        int maxAttemptsPerBeacon = 50;   // prevents infinite loops

        List<Vector3> placedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfBeacons; i++)
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
                    if (Vector3.Distance(candidatePos, pos) < minDistance)
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
        if (distancePlayerMonster <= monsterObservationRange)
        {
            // Check Player Imitation with every beacon in range
            for (int i = 0; i < activeCreatureBeacons.Count; i++)
            {
                float distancePlayerBeacon = Vector3.Distance(playerObject.transform.position, activeCreatureBeacons[i].transform.position);
                CreatureBeaconScript beaconScript = activeCreatureBeacons[i].GetComponent<CreatureBeaconScript>();
                if (distancePlayerBeacon <= beaconScript.range)
                {
                    //  Compare Player Movement Data with Creature Beacon Expected Movement Data
                    if(playerScript.movementData.Count > 0 && beaconScript.movementData.Count > 0)
                    {
                        if (playerScript.movementData.Equals(beaconScript.movementData))
                        {
                            imitationCorrect = true;
                        }
                    }

                    //  Compare Player Behavior Data with Monster Expected Behavior Data
                    if (playerScript.behaviorData.Count > 0 && beaconScript.behaviorData.Count > 0)
                    {
                        if (playerScript.behaviorData.Equals(beaconScript.behaviorData))
                        {
                            imitationCorrect = true;
                        }
                    }
                }
            }
        }

        if (!imitationCorrect)
        {
            // Start or Accelerate Monster Cool Down
            monsterScript.timeRemaining -= Time.deltaTime * coolDownAccelerationFactor; // Accelerate cool down
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
