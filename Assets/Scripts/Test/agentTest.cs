using UnityEngine;
using UnityEngine.AI;

public class MouseFollowNavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera cam;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        // Ray from camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if ray hits something (like the ground)
        if (Physics.Raycast(ray, out hit))
        {
            // Continuously update the agent destination
            agent.SetDestination(hit.point);
            Debug.Log("hit");
        }
    }
}