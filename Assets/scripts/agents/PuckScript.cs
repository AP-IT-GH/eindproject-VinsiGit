using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.MLAgents;

public class PuckScript : MonoBehaviour
{
    // public GameObject red; //red
    public GameObject blue; //blue
    public GameObject redGoal;
    public GameObject blueGoal;

    private HockeyAgent redAgent; //red agent
    private HockeyAgent blueAgent; //blue agent
    public TMP_Text scoreRedText;
    public TMP_Text scoreBlueText;

    // Keeping track of the scores
    private int blueScore = 0;
    private int redScore = 0;

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
        // redAgent = red.GetComponent<HockeyAgent>();
        blueAgent = blue.GetComponent<HockeyAgent>();
        UpdateScoreText();
    }
    private void OnTriggerEnter(Collider other)
    {

        float reward = 1f;

        // Check if the entering collider is the red's goal
        if (other.gameObject == redGoal)
        {
            // redAgent.AddReward(-1f*reward); 
            blueAgent.AddReward(reward); 
            // redAgent.EndEpisode(); // End the episode for agent1
            blueAgent.EndEpisode(); // End the episode for agent2
            blueScore++;
            UpdateScoreText();
        }
        // Check if the entering collider is the blue's goal
        else if (other.gameObject == blueGoal)
        {
            // redAgent.AddReward(reward); 
            blueAgent.AddReward(-1f*reward); 
            // redAgent.EndEpisode(); // End the episode for agent1
            blueAgent.EndEpisode(); // End the episode for agent2
            redScore++; 
            UpdateScoreText();
        }
    }    
    void UpdateScoreText()
    {
        if (scoreRedText != null)
        {
            // scoreText.text = "Red: " + redScore.ToString() + " - Blue: " + blueScore.ToString();
            scoreRedText.text = "Red: " + redScore.ToString();
        }
        if (scoreBlueText != null)
        {
            scoreBlueText.text = "Blue: " + blueScore.ToString();
        }
    }

}
