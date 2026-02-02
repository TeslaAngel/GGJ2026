using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private float timer = 1f;

    public List<string> behaviors = new List<string>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // reset all parameter
            animator.SetBool("I", false);
            animator.SetBool("J", false);
            animator.SetBool("L", false);

            // read next from behavior
            string nextBehavior = behaviors[0];
            animator.SetBool(nextBehavior, true);
            behaviors.RemoveAt(0);
            behaviors.Add(nextBehavior);

            // reset timer
            timer = 1f;
        }
    }
}
