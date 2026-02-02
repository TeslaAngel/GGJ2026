using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBeaconScript : MonoBehaviour
{

    public float range;
    public int maskIndex;
    
    public float activeDuration = 5.0f;
    public GameObject creaturePrefab;
    public int creatureNum = 8;

    [Space]
    public List<char> behaviorData = new List<char>();

    public List<char> movementData = new List<char>();

    private void Start()
    {
        for(int i = 0; i < creatureNum; i++)
        {
            Vector3 randomV3 = Random.onUnitSphere * Random.Range(-range, range);
            randomV3 = new Vector3(randomV3.x, randomV3.y, 0);
            GameObject creature = Instantiate(creaturePrefab, transform.position + randomV3, Quaternion.identity);
        }
    }
}
