using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

using Random = UnityEngine.Random;

public class HockeyAgent : Agent
{
    public GameObject puck;
    public GameObject enemy;

    public Transform OptimalPosition;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;
    private float timeLimit = 20f; // Set this to the amount of time you want

    private Rigidbody agentRb;
    private Transform agentTf;

    private Rigidbody puckRb; // Reference to the Rigidbody component of the puck
    private Transform puckTf;
    private Vector3 lastAgentPosition; // Store the agent's last position for collision detection

    private bool scoreMade;

    private int teamId;


    // Called when the Agent starts
    public override void Initialize()
    {
        
        agentRb = GetComponent<Rigidbody>();
        agentTf = GetComponent<Transform>();
        puckRb = puck.GetComponent<Rigidbody>(); // Get the Rigidbody component from the puck
        puckTf = puck.GetComponent<Transform>();
        lastAgentPosition = agentRb.position;

        teamId  = GetComponent<BehaviorParameters>().TeamId;
    }
    
    public override void OnEpisodeBegin()
    {
        // Reset puck's position
        puckTf.localPosition = new Vector3(Random.value * 5, 0.7f, Random.value * 7 - 3.5f);

        // Random rotation between 50 and 60 degrees
        float randomRotationAngle = Random.Range(50f, 60f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomRotationAngle, 0f);

        // Apply random rotation to the puck
        puckTf.localRotation = randomRotation;

        // Define the magnitude factor for the force
        float forceMagnitude = 5f; // You can adjust this value as needed

        // Generate a random direction vector
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Apply the magnitude factor to the direction vector
        Vector3 randomForce = randomDirection * forceMagnitude;

        // Reset the puck's velocity
        puckRb.velocity = Vector3.zero;

        // Apply the force to the puck
        puckRb.AddForce(randomForce, ForceMode.Impulse);

        scoreMade = false;

        // Start the ScoreCheck coroutine
        StartCoroutine(ScoreCheck());

    }

    public override void CollectObservations(VectorSensor sensor) //3 normal, 8 with puck , 9 now
    {
        // sensor.AddObservation(agentRb.position);
        sensor.AddObservation(agentTf.localPosition.x);
        sensor.AddObservation(agentTf.localPosition.z);

        sensor.AddObservation(enemy.transform.localPosition.x);
        sensor.AddObservation(enemy.transform.localPosition.z);

        sensor.AddObservation(puckTf.localPosition.x);
        sensor.AddObservation(puckTf.localPosition.z);

        sensor.AddObservation(puckRb.velocity.z);
        sensor.AddObservation(puckRb.velocity.x);
        float distanceToPuck = Vector3.Distance(agentTf.position, puckTf.position);
        sensor.AddObservation(distanceToPuck);

        }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;

        // Move sideways
        float moveActionSW = continuousActions[1]* 0.5f; // Assuming sideways movement is at index 0
        Vector3 moveDirectionSW = transform.right * moveActionSW * moveSpeed;

        // Move forward or backward
        float moveActionFW = continuousActions[0]* 0.5f; // Assuming forward/backward movement is at index 1
        Vector3 moveDirectionFW = transform.forward * -moveActionFW * moveSpeed;

        // Apply the movement forces
        agentRb.AddForce(moveDirectionSW, ForceMode.VelocityChange);
        agentRb.AddForce(moveDirectionFW, ForceMode.VelocityChange);
        // Add any reward logic here based on the actions
        float distanceToTarget = Vector3.Distance(agentTf.localPosition, OptimalPosition.position);
    
        if (teamId == 0 && agentTf.localPosition.x >-1.0f) // If the agent is on team 0 and has moved to the positive side of the table
        {
            agentTf.localPosition = new Vector3(-1.0f, agentTf.localPosition.y, agentTf.localPosition.z); // Reset the x position to 0
        }
        else if (teamId == 1 && agentTf.localPosition.x < 1.0f) // If the agent is on team 1 and has moved to the negative side of the table
        {
            agentTf.localPosition = new Vector3(1.0f, agentTf.localPosition.y, agentTf.localPosition.z); // Reset the x position to 0
        }
        
        float distanceToPuck = Vector3.Distance(agentTf.localPosition, puck.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Puck"))
        {
            scoreMade = true;
            StopCoroutine(ScoreCheck());

            AddReward(0.1f);
            // Calculate the movement vector of the parent object
            Vector3 agentMovement = agentRb.position - lastAgentPosition;
            lastAgentPosition = agentRb.position;

            // Add the movement vector of the parent object to the movement vector of the puck
            Rigidbody puckRb = collision.gameObject.GetComponent<Rigidbody>();
            if (puckRb != null)
            {
                puckRb.velocity += agentMovement*0.1f;
            }
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        // Control rotation and movement using keyboard inputs for testing
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private System.Collections.IEnumerator ScoreCheck()
    {
        // Wait for the time limit
        yield return new WaitForSeconds(timeLimit);

        // If no score was made, give a -0.1 reward
        if (!scoreMade)
        {
            // AddReward(-0.1f);
            EndEpisode();
        }
    }
    
}
