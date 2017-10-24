using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        // Destroy object after 1 second.
        Destroy(this.gameObject, 1.0f);
    }

    private void Explode()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        particle.Play();
    }
}