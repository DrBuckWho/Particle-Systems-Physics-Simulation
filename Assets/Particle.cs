using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject plane;
    private Vector3 velocity;
    public float mass = 1f;
    private int bounces = 0;
    private int maxBounces = 4; // Number of times particle can bounce before being deleted
    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 acceleration;

    // Update is called once per frame
    void Update()
    {
        // Update position based on velocity and acceleration
        transform.position += velocity * Time.deltaTime;
        ApplyForce(gravity * mass); // Apply gravity
        velocity += acceleration * Time.deltaTime;

        // Check collision with plane
        CheckCollisionWithPlane();

        // Reset acceleration for the next frame
        acceleration = Vector3.zero;
    }

    // Set velocity of the particle
    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    public void ApplyForce(Vector3 force)
    {
        // Apply force to the object
        acceleration += force / mass;
    }

    // Check collision with plane
    private void CheckCollisionWithPlane()
    {
        // Assuming the plane is at y = 0, check if the particle is below the plane
        if (transform.position.y <= 0f)
        {
            Vector3 planeNormal = plane.transform.up.normalized;
            Vector3 relativeVelocity = -Vector3.Dot(planeNormal, velocity) * planeNormal;
            Vector3 reflectionDirection = velocity + 2 * relativeVelocity;
            // Reverse the y-component of velocity to simulate bouncing
            Quaternion inverseRotation = Quaternion.Inverse(plane.transform.rotation);
            reflectionDirection = inverseRotation * reflectionDirection;

            // Step 5: Apply the reflection direction to the particle's velocity with a bounce factor
            velocity = 0.8f * reflectionDirection;

            // Increment bounce count
            bounces++;
            // If exceeded maximum bounces, delete the particle
            if (bounces >= maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
  }
