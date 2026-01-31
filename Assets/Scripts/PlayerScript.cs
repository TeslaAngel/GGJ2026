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
    private int movementDataRecordLength = 5; 
    //  We maintain a list of characters representing player inputs on movement
    private List<char> movementData = new List<char>();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") * verticalSpeedModifier;

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
