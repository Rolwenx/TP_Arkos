using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    public Animator lampAnimator;
    public AudioSource lightSound;
    private bool hasBeenActivated = false;

    public void PlayLightSound()
    {
        if (lightSound != null)
            lightSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!hasBeenActivated && other.CompareTag("Player"))
        {
            hasBeenActivated = true;

            lampAnimator.SetTrigger("TurnOn");
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (hasBeenActivated && other.CompareTag("Player"))
        {
            hasBeenActivated = false;

            lampAnimator.SetTrigger("TurnOff");
        }

    }
}
