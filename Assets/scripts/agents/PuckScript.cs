using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PuckScript : MonoBehaviour
{
    public GameObject player; //player
    public GameObject playerGoal;
    public GameObject enemyGoal;

    private HockeyAgent playerAgent; //player agent
    private HockeyAgent enemyAgent; //enemy agent

    public float reward=1;

    void Start () {
        playerAgent = player.GetComponent<HockeyAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {

        // Check if the entering collider is the player's goal
        if (other.gameObject == playerGoal)
        {
            playerAgent.AddReward(-reward); 
            playerAgent.EndEpisode(); // End the episode for agent1
        }
        // Check if the entering collider is the enemy's goal
        else if (other.gameObject == enemyGoal)
        {
            playerAgent.AddReward(reward); 
            playerAgent.EndEpisode(); // End the episode for agent1
        }
    }
}
