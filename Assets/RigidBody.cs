using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{
    public GameObject otherObject;
    public Vector3 velocity;
    public float mass = 1f;
    private Vector3 acceleration;
    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);

    void Start()
    {
        acceleration = Vector3.zero;
        otherObject = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cubePosition = otherObject.transform.position;
        Vector3 cubeScale = otherObject.transform.lossyScale;

        Quaternion cubeRotation = otherObject.transform.rotation;

        // Calculate cube's bounds in world space
        Vector3 cubeMinBounds = cubePosition - cubeScale / 2f;
        Vector3 cubeMaxBounds = cubePosition + cubeScale / 2f;

        // Convert sphere's position to local space of the cube
        Vector3 sphereLocalPosition = Quaternion.Inverse(cubeRotation) * (transform.position - cubePosition);

        float sphereRadius = transform.localScale.x / 2f;

        // Check for collision in local space
        if (sphereLocalPosition.x + sphereRadius >= cubeMinBounds.x &&
            sphereLocalPosition.x - sphereRadius <= cubeMaxBounds.x &&
            sphereLocalPosition.y + sphereRadius >= cubeMinBounds.y &&
            sphereLocalPosition.y - sphereRadius <= cubeMaxBounds.y &&
            sphereLocalPosition.z + sphereRadius >= cubeMinBounds.z &&
            sphereLocalPosition.z - sphereRadius <= cubeMaxBounds.z)
        {
            // Collision detected
        }
        else
        {
            ApplyForce(gravity * mass);
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        // Apply force to the object
        acceleration += force / mass;
    }
}
