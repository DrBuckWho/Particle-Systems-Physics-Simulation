using UnityEngine;

public class Particle : MonoBehaviour
{
    private Vector3 velocity;
    private bool grounded = false;
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
            // Reverse the y-component of velocity to simulate bouncing
            velocity.y *= -1;
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
