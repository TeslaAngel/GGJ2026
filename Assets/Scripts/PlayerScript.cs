using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
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
    public List<char> movementData = new List<char>();

    public float movementCooldown = 1.0f;
    private float movementCooldownTimer;

    [Space]
    // Behavior Data Record
    public int behaviorDataRecordLength = 5;
    //  We maintain a list of characters representing player inputs on movement
    public List<char> behaviorData = new List<char>();

    public float behaviorCooldown = 1.0f;
    private float behaviorCooldownTimer;

    [Space]
    // Mask Data
    public int maskIndex = 0;
    public int maskCount = 3;
    public bool[] maskEnable;

    [Space]
    // Animation #SUBJECT TO CHANGE
    private Animator animator; // we need the flexibility to change the style to any creature, maybe a tree?


    // Start is called before the first frame update
    void Start()
    {
        // init & enable all masks
        maskEnable = new bool[maskCount+1];
        for (int i = 0; i < maskCount+1; i++)
        {
            maskEnable[i] = true;
        }
        // 0 is no mask, always availables
        maskEnable[0] = true;

        // Rigidbody
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Animator #SUBJECT TO CHANGE
        animator = GetComponent<Animator>();
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
            movementData.Add('W');
            movementCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetInteger("MoveDir", 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movementData.Add('A');
            movementCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetInteger("MoveDir", 2);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movementData.Add('S');
            movementCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetInteger("MoveDir", 3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movementData.Add('D');
            movementCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetInteger("MoveDir", 4);
        }
        // when player haven't moved in another direction for a while, record a 'no movement' character
        else
        {
            movementCooldownTimer += Time.deltaTime;
            if (movementCooldownTimer >= movementCooldown)
            {
                movementData.Add('n'); // N for No movement
                movementCooldownTimer = 0.0f;
            }
        }

        // set animator walking parameter
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        // Maintain fixed length of movement data record
        while (movementData.Count > movementDataRecordLength)
        {
            movementData.RemoveAt(0);
        }

        // behavior Record #SUBJECT TO CHANGE
        if (Input.GetKeyDown(KeyCode.I))
        {
            behaviorData.Add('I'); // jump behavior
            behaviorCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetTrigger("I");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            behaviorData.Add('J'); // left hand behavior
            behaviorCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetTrigger("J");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            behaviorData.Add('L'); // right hand behavior
            behaviorCooldownTimer = 0.0f;

            // sync changes to animator
            animator.SetTrigger("L");
        }
        // when player haven't performed any behavior for a while, record a 'no behavior' character
        else
        {
            behaviorCooldownTimer += Time.deltaTime;
            if (behaviorCooldownTimer >= behaviorCooldown)
            {
                behaviorData.Add('n'); // N for No behavior
                behaviorCooldownTimer = 0.0f;
            }
        }
        // Maintain fixed length of behavior data record
        while (behaviorData.Count > behaviorDataRecordLength)
        {
            behaviorData.RemoveAt(0);
        }

        // Mask rolling (from 0 (no mask) to maskCount then to 0, skip any mask that's not enabled)
        bool maskRollTrigger = Input.GetButtonDown("Jump");
        if (maskRollTrigger)
        {
            int startIndex = maskIndex;

            do
            {
                maskIndex = (maskIndex + 1) % (maskEnable.Length);
                // sync changes to animator
                animator.SetInteger("MaskIndex", maskIndex);
                if (maskEnable[maskIndex])
                    break;

            } while (maskIndex != startIndex);
        }
    }
}
