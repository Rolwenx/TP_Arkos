using UnityEngine;

public class FireActivator : MonoBehaviour
{
    private ParticleSystem[] allParticles;
    public AudioSource fireAudio; 

    private void Start()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();

        // on éteint le système de particule si c'est allumé
        foreach (ParticleSystem ps in allParticles)
        {
            ps.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ParticleSystem ps in allParticles)
            {
                ps.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ParticleSystem ps in allParticles)
            {
                ps.Stop();
            }
        }
    }
}
