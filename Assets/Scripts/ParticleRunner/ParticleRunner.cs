using UnityEngine;

public class ParticleRunner : MonoBehaviour
{
    [SerializeField] ParticleSystem[] gunParticles;

    public void GunParticles()
    {
        foreach (var system in gunParticles)
        {
            system.Play();
        }
    }
}