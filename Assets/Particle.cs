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
      // Get the plane's normal direction
      Vector3 planeNormal = plane.transform.up.normalized;

      // Calculate the position of the particle relative to the plane
      Vector3 particleToPlane = transform.position - plane.transform.position;

      // Project the relative position onto the plane's local coordinates
      Vector3 localPosition = Quaternion.Inverse(plane.transform.rotation) * particleToPlane;

      // Check if the particle is below the plane (in local coordinates)
      if (localPosition.y <= 0f)
      {
        transform.position.y =0;
          // Calculate the reflection direction in local coordinates
          Vector3 reflectionDirection = Vector3.Reflect(velocity, planeNormal);

          // Convert the reflection direction to world coordinates
          reflectionDirection = plane.transform.rotation * reflectionDirection;

          // Apply the reflection direction to the particle's velocity with a bounce factor
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
