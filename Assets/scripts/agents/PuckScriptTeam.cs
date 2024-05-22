using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PuckScriptTeam : MonoBehaviour
{
    public GameObject player; //player
    
    public GameObject enemy; //player

    public GameObject playerGoal;
    public GameObject enemyGoal;

    private HockeyAgentTeam playerAgent; //player agent
    private HockeyAgentTeam enemyAgent; //enemy agent

    public float reward=1;

    void Start () {
        playerAgent = player.GetComponent<HockeyAgentTeam>();
        enemyAgent = enemy.GetComponent<HockeyAgentTeam>();

    }
    private void OnTriggerEnter(Collider other)
    {

        // Check if the entering collider is the player's goal
        if (other.gameObject == playerGoal)
        {
            playerAgent.AddReward(reward); 
            enemyAgent.AddReward(-reward); 

            playerAgent.EndEpisode(); // End the episode for agent1
            enemyAgent.EndEpisode(); // End the episode for agent1

        }
        // Check if the entering collider is the enemy's goal
        else if (other.gameObject == enemyGoal)
        {
            playerAgent.AddReward(reward); 
            enemyAgent.AddReward(-reward); 

            playerAgent.EndEpisode(); // End the episode for agent1
            enemyAgent.EndEpisode(); // End the episode for agent1

        }
    }
}
