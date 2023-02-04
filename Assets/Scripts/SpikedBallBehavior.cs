using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallBehavior : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float angularSpeed = 10f;
    public float rotationLimit = 90f;

    public GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, head.transform.position) < .1f)
        {
            currentWaypointIndex++;
            angularSpeed *= -1;
            if (currentWaypointIndex == waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        rigidBody.angularVelocity = angularSpeed;
    }

}
