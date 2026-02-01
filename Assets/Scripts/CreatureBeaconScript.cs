using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBeaconScript : MonoBehaviour
{

    public float range;
    public int maskIndex;

    [Space]
    public List<char> behaviorData = new List<char>();
    //public Queue<char> behaviorData = new Queue<char>();

    public List<char> movementData = new List<char>();
    //public Queue<char> movementData = new Queue<char>();

    /*// Start is called before the first frame update
    void Start()
    {
        foreach(char c in initBehaviorData)
        {
            behaviorData.Enqueue(c);
        }

        foreach (char c in initMovementData)
        {
            movementData.Enqueue(c);
        }
    }*/
}
