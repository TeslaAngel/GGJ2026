using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    // This script spawns creature prefabs at runtime. The Animator component for
    // each creature is assigned a different Animator Controller based on the creature type.

    // The prefab of the creature Game Object containing an Animator component.
    public GameObject firstCreaturePrefab;
    public GameObject secondCreaturePrefab;
    public GameObject thirdCreaturePrefab;

    // Store the references to the Animator Controllers for the different creature types that can be spawned
    public RuntimeAnimatorController firstCreatureAnimator;
    public RuntimeAnimatorController secondCreatureAnimator;
    public RuntimeAnimatorController thirdCreatureAnimator;

    void Start()
    {
        for (var i = 0; i < 6; i++)
        {
            SpawnCreature();
        }
    }

    public void SpawnCreature()
    {
        // Instantiate a new creature Game Object at a random position
        //GameObject creature = Instantiate(creaturePrefab, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 10f)), Quaternion.identity);
        //Animator animator = creature.GetComponent<Animator>();

        // Randomly determine the type of creature and instantiate a new creature Game Object at a random position
        // Assign its Animator with its corresponding Animator Controller.
        var randomValue = Random.Range(0f, 1f);
        if (randomValue <= 0.3f)
        {
            GameObject creature = Instantiate(secondCreaturePrefab, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 10f)), Quaternion.identity);
            Animator animator = creature.GetComponent<Animator>();
            animator.runtimeAnimatorController = secondCreatureAnimator;
            //creature.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);  //for bigger size
        }
        else if (randomValue <= 0.6f)
        {
            GameObject creature = Instantiate(thirdCreaturePrefab, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 10f)), Quaternion.identity);
            Animator animator = creature.GetComponent<Animator>();
            animator.runtimeAnimatorController = thirdCreatureAnimator;
            //creature.transform.localScale = new Vector3(0.60f, 0.60f, 0.60f);  //for smaller size
        }
        else
        {
            GameObject creature = Instantiate(firstCreaturePrefab, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 10f)), Quaternion.identity);
            Animator animator = creature.GetComponent<Animator>();
            animator.runtimeAnimatorController = firstCreatureAnimator;
            //creature.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);  //for standard size
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
