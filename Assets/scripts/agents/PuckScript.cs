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
    public AudioClip hitSound;
    public float maxPuckSpeed = 10f;
    public float maxVolume = 1f;
    public float minSpeedChangeForSound = 2f; // Minimale snelheidsverandering om als 'hit' te tellen

    // Keeping track of the scores
    private bool canPlaySound = true;
    private int blueScore = 0;
    private int redScore = 0;
    private Vector3 lastFrameVelocity;

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

    void Update()
    {
        Rigidbody puckRb = GetComponent<Rigidbody>();

        // Bereken de snelheidsverandering
        float velocityChange = (puckRb.velocity - lastFrameVelocity).magnitude;

        // Speel geluid af alleen bij een significante snelheidsverandering en als de cooldown voorbij is
        if (canPlaySound && velocityChange > minSpeedChangeForSound)
        {
            float hitIntensity = velocityChange / maxPuckSpeed;
            float volume = Mathf.Min(hitIntensity, maxVolume);
            AudioSource.PlayClipAtPoint(hitSound, transform.position, volume);

            StartCoroutine(SoundCooldown());
        }

        // Beperk de snelheid van de puck
        if (puckRb.velocity.magnitude > maxPuckSpeed)
        {
            puckRb.velocity = puckRb.velocity.normalized * maxPuckSpeed;
        }

        // Update lastFrameVelocity voor de volgende frame
        lastFrameVelocity = puckRb.velocity;
    }

    IEnumerator SoundCooldown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(0.1f); // Kortere cooldown om snelle opeenvolgende hits toe te staan
        canPlaySound = true;
    }

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
