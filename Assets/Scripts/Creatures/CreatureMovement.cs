using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Transform target; // e.g., the player's transform

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Find the player or set target through the inspector
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction towards target
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;

        // Set animation parameters (e.g., for a Blend Tree or simply a running bool)
        anim.SetFloat("Horizontal", movement.x);
        anim.SetBool("isRunning", movement.magnitude > 0);

        // Flip the sprite if moving left
        if (movement.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D for physics consistency
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
