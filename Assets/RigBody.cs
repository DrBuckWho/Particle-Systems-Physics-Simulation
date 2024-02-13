using UnityEngine;

public class RigBody : MonoBehaviour
{
    public float collisionRadius = 0.5f; // Radius of the sphere

    private void Update()
    {
        // Check for collisions with other spheres
        CheckSphereCollision();
    }

    private void CheckSphereCollision()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Particle"); // Assuming your spheres have a tag "Sphere"

        foreach (GameObject sphere in spheres)
        {
            if (sphere != gameObject) // Avoid self-collision
            {
                float distance = Vector3.Distance(transform.position, sphere.transform.position);

                if (distance < collisionRadius * 2) // If distance is less than twice the radius, collision occurs
                {
                    // Handle collision response
                    HandleCollisionResponse(sphere.transform.position);
                }
            }
        }
    }

    private void HandleCollisionResponse(Vector3 collisionPosition)
    {
        // Example: Bounce back when colliding with another sphere
        Vector3 direction = transform.position - collisionPosition;
        transform.position += direction.normalized * Time.deltaTime; // Move away from the collision point
    }
}
