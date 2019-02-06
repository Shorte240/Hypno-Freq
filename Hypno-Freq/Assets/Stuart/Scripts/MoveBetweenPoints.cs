using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBetweenPoints : MonoBehaviour
{

    public Transform[] points;
    private int destPointIndex = 0;
    public NavMeshAgent agent;
    public Vector3 StartVec;
    public Vector3 EndVec;
    public GameStateManager gameStateManager;

    public bool hypno = false;
    bool alive = true;
    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        gameStateManager = GetComponent<GameStateManager>();
        agent.updateRotation = false;
        
    }
    void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPointIndex].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPointIndex = (destPointIndex + 1) % points.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if (tag == "Adult")
        {
            Debug.Log(
                agent.tag +
                "\ngoal: " + agent.pathEndPosition +
                "\tcurrent pos: " + transform.position +
                "\tdistance: " + Vector3.Distance(agent.pathEndPosition, transform.position)
            );
        }

        // stop when reached hypno device
        if (hypno && Vector3.Distance(agent.pathEndPosition, transform.position) < 80)
        {
            if (tag == "Adult")
            {
                Debug.Log("reached device");
            }

            if (alive)
            {
                GameObject.Find("GameStateManager").GetComponent<GameStateManager>().aliveCount++;
                alive = false;
            }

            agent.isStopped = true;
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (tag == "Adult")
            {
                Debug.Log("going to next point"); 
            }
            GoToNextPoint();
        }
        else
        {
            if (tag == "Adult")
            {
                Debug.Log("What's happening here?"); 
            }
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag && other.gameObject.tag != "Car")
        {
            Debug.Log("Trigger");

            if (!hypno)
            {
                Debug.Log(agent.tag + " hypnotized");
                agent.SetDestination(other.transform.position); 
                hypno = true;
                //GameObject.Find("GameStateManager").GetComponent<GameStateManager>().aliveCount++;
            }
        }
    }
}