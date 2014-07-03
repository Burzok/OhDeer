using UnityEngine;
using System.Collections;

public class LeafParticles : MonoBehaviour
{

    private float speed;

    void Start()
    {
        particleSystem.renderer.sortingLayerName = "Particles";
        speed = 0.2f;
    }

    void Update()
    {
        AddForceOnParticles();
    }

    private void AddForceOnParticles()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        this.particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            float moveHorizontal = Input.acceleration.normalized.x;
            particles[i].velocity += new Vector3(moveHorizontal, 0.0f, 0.0f) * speed;
        }

        this.particleSystem.SetParticles(particles, particles.Length);
    }
}
