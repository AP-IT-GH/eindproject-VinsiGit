using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCKeeper : MonoBehaviour
{

    private Rigidbody keeperRb;
    private Transform keeperTf;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        keeperRb = GetComponent<Rigidbody>();
        keeperTf = GetComponent<Transform>();

        startPosition = keeperTf.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Random.Range(0f, 60f) <= 1)
        {
            Vector3 randomForce = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            keeperRb.AddForce(randomForce, ForceMode.VelocityChange);
        }

        if (keeperTf.localPosition.x > -1.0f) // If the agent is on team 0 and has moved to the positive side of the table
        {
            keeperTf.localPosition = new Vector3(-1.0f, keeperTf.localPosition.y, keeperTf.localPosition.z); // Reset the x position to 0
        }
    }

    public void Reset()
    {
        keeperTf.localPosition = startPosition;
        keeperRb.velocity = Vector3.zero;
    }
}
