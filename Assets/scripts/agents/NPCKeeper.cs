using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCKeeper : MonoBehaviour
{

    private Rigidbody keeperRb;

    // Start is called before the first frame update
    void Start()
    {
        keeperRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 60f) <= 1)
        {
            Vector3 randomForce = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            keeperRb.AddForce(randomForce, ForceMode.VelocityChange);
        }
    }
}
