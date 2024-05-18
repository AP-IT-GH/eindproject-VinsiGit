using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PuckScript : MonoBehaviour
{
    public GameObject player; //player
    public GameObject enemy; //enemy
    public GameObject playerGoal;
    public GameObject enemyGoal;
    public Transform playerOptimalPosition;
    public Transform enemyOptimalPosition;

    private HockeyAgent playerAgent; //player agent
    private HockeyAgent enemyAgent; //enemy agent
    private Transform playerTransform; //player transform
    private Transform enemyTransform; //enemy transform

    // private HockeyAgent lastAgentToHitPuck; // Keep track of the last agent that hit the puck

    // private void OnCollisionEnter(Collision collision)
    // {
    //     HockeyAgent agent = collision.gameObject.GetComponent<HockeyAgent>();

    //     // If the puck was hit by an agent and it's not the same agent that hit it last time
    //     if (agent != null && agent != lastAgentToHitPuck)
    //     {
    //         // Reward the agent for intercepting the puck
    //         agent.AddReward(0.1f);

    //         // Update the last agent that hit the puck
    //         lastAgentToHitPuck = agent;
    //     }
    // }
    void Start () {
        playerAgent = player.GetComponent<HockeyAgent>();
        enemyAgent = enemy.GetComponent<HockeyAgent>();
        playerTransform = player.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {

        float distanceToOptimalPositionPlayer = playerTransform.position.x-playerOptimalPosition.position.x;
        float distanceToOptimalPositionEnemy = enemyTransform.position.x-enemyOptimalPosition.position.x;

        float reward1 = 1f / (1f + distanceToOptimalPositionPlayer);
        float reward2 = 1f / (1f + distanceToOptimalPositionEnemy);

        // Check if the entering collider is the player's goal
        if (other.gameObject == playerGoal)
        {
            playerAgent.AddReward(-1f + reward1); 
            enemyAgent.AddReward(1f + reward2); 
            playerAgent.EndEpisode(); // End the episode for agent1
            enemyAgent.EndEpisode(); // End the episode for agent2
        }
        // Check if the entering collider is the enemy's goal
        else if (other.gameObject == enemyGoal)
        {
            playerAgent.AddReward(1f+reward1); 
            enemyAgent.AddReward(-1f+reward2); 
            playerAgent.EndEpisode(); // End the episode for agent1
            enemyAgent.EndEpisode(); // End the episode for agent2
        }
    }
}
