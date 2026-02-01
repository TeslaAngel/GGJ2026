using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Movement Data
    public float speed = 5.0f;
    public float verticalSpeedModifier = 0.5f;
    private Rigidbody2D rigidBody2D;

    [Space]
    // Movement Data Record
    public int movementDataRecordLength = 5;
    //  We maintain a list of characters representing player inputs on movement
    public Queue<char> movementData = new Queue<char>();

    public float movementCooldown = 1.0f;
    private float movementCooldownTimer;

    [Space]
    // Behavior Data Record
    public int behaviorDataRecordLength = 5;
    //  We maintain a list of characters representing player inputs on movement
    public Queue<char> behaviorData = new Queue<char>();

    public float behaviorCooldown = 1.0f;
    private float behaviorCooldownTimer;

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

        // Rigidbody
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") * verticalSpeedModifier;

        Vector3 movement = new Vector2(horizontalInput, verticalInput);
        //transform.Translate(movement * speed * Time.deltaTime);
        rigidBody2D.velocity = movement * speed;

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
        // when player haven't moved in another direction for a while, record a 'no movement' character
        else
        {
            movementCooldownTimer += Time.deltaTime;
            if (movementCooldownTimer >= movementCooldown)
            {
                movementData.Enqueue('n'); // N for No movement
                movementCooldownTimer = 0.0f;
            }
        }
        // Maintain fixed length of movement data record
        if (movementData.Count > movementDataRecordLength)
        {
            movementData.Dequeue();
        }

        // behavior Record #SUBJECT TO CHANGE
        if (Input.GetKeyDown(KeyCode.I))
        {
            behaviorData.Enqueue('I'); // jump behavior
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            behaviorData.Enqueue('J'); // left hand behavior
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            behaviorData.Enqueue('L'); // right hand behavior
        }
        // when player haven't performed any behavior for a while, record a 'no behavior' character
        else
        {
            behaviorCooldownTimer += Time.deltaTime;
            if (behaviorCooldownTimer >= behaviorCooldown)
            {
                behaviorData.Enqueue('n'); // N for No behavior
                behaviorCooldownTimer = 0.0f;
            }
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
