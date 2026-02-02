using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBeaconScript : MonoBehaviour
{

    public float range;
    public int maskIndex;
    
    public float activeDuration = 5.0f;

    [Space]
    public List<char> behaviorData = new List<char>();

    public List<char> movementData = new List<char>();
}
