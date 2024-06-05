using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScriptGame : MonoBehaviour
{
    public GameObject playerGoal;
    public GameObject enemyGoal;

    public float reward=1;
    private Vector3 positionReset ;
    private Rigidbody puckRigidbody;

    void Start () {
        positionReset = transform.position;
        puckRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {

        // Check if the entering collider is the player's goal
        if (other.gameObject == (playerGoal || enemyGoal))
        {       
             transform.position = positionReset;
             puckRigidbody.velocity = Vector3.zero; // Set speed to 0
        }
        // // Check if the entering collider is the enemy's goal
        // else if (other.gameObject == enemyGoal)
        // {   
        //          transform.position = positionReset;

        // }
    }
}