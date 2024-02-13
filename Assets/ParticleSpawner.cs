using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab for the particle object
    public Transform spawnPoint; // Point from which particles will be spawned
    public float spawnRate = 1.0f; // Rate of spawning particles (particles per second)
    public float particleSpeed = 5.0f; // Initial speed of particles

    private float timer; // Timer to keep track of spawning

    private void Update()
    {
        // Spawn particles according to spawn rate
        timer += Time.deltaTime;
        if (timer >= 1f / spawnRate) // Check if enough time has passed to spawn a particle
        {
            SpawnParticle();
            timer = 0f;
        }
    }

    private void SpawnParticle()
    {
        // Instantiate the particle prefab at the spawn point
        GameObject newParticle = Instantiate(particlePrefab, spawnPoint.position, Quaternion.identity);

        // Calculate initial velocity for the particle
        Vector3 velocity = Random.onUnitSphere * particleSpeed;
        //Vector3 velocity = Vector3.zero;


        // Set the velocity to the particle
        Particle particleComponent = newParticle.GetComponent<Particle>();
        if (particleComponent != null)
        {
            particleComponent.SetVelocity(velocity);
        }
    }
}
