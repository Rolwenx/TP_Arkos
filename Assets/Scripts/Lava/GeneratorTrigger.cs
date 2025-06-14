using UnityEngine;
using UnityEngine.AI; 
using Unity.AI.Navigation; 


public class GeneratorTrigger : MonoBehaviour
{
    public NavMeshSurface surface;
    public AudioSource fxAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] barriers = GameObject.FindGameObjectsWithTag("Barrier");
            foreach (GameObject barrier in barriers)
            {
                barrier.SetActive(false);
            }

            if (surface != null)
            {
                surface.BuildNavMesh();
            }
            if (fxAudio != null)
            fxAudio.Play();
        }
    }
}
