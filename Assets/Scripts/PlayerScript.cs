using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Movement Data
    public float speed = 5.0f;
    public float verticalSpeedModifier = 0.5f;

    [Space]
    // Movement Data Record
    public int movementDataRecordLength = 5;
    //  We maintain a list of characters representing player inputs on movement
    public Queue<char> movementData = new Queue<char>();

    [Space]
    // Behavior Data Record
    public int behaviorDataRecordLength = 5;
    //  We maintain a list of characters representing player inputs on movement
    public Queue<char> behaviorData = new Queue<char>();

    [Space]
    // Mask Data
    public int maskIndex = 0;
    public int maskCount = 3;
    public bool[] maskEnable;

    [Space]
    // Animation
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        // init & disable all masks
        maskEnable = new bool[maskCount+1];
        for (int i = 0; i < maskCount; i++)
        {
            maskEnable[i] = false;
        }
        // 0 is no mask, always availables
        maskEnable[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") * verticalSpeedModifier;

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Movement Record
        if (Input.GetKeyDown(KeyCode.W))
        {
            movementData.Enqueue('W');
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movementData.Enqueue('A');
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movementData.Enqueue('S');
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movementData.Enqueue('D');
        }
        // Maintain fixed length of movement data record
        if (movementData.Count > movementDataRecordLength)
        {
            movementData.Dequeue();
        }

        // behavior Record #SUBJECT TO CHANGE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            behaviorData.Enqueue('J'); // Jump
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            behaviorData.Enqueue('A'); // Attack
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            behaviorData.Enqueue('I'); // Interact
        }
        // Maintain fixed length of behavior data record
        if (behaviorData.Count > behaviorDataRecordLength)
        {
            behaviorData.Dequeue();
        }

        // Mask rolling (from 0 (no mask) to maskCount then to 0, skip any mask that's not enabled)
        bool maskRollTrigger = Input.GetButtonDown("Jump");
        if (maskRollTrigger)
        {
            int startIndex = maskIndex;

            do
            {
                maskIndex = (maskIndex + 1) % maskEnable.Length;
                if (maskEnable[maskIndex])
                    break;

            } while (maskIndex != startIndex);
        }
    }
}
