using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class StormController : MonoBehaviour
{
    public ParticleSystem stormSnow;
    public ParticleSystem stormFog;
    public ParticleSystem normalSnow;
    public Light globalLight;
    public GameObject door;
    public GameObject door2;
    public AudioSource clickSound;
    public TextMeshProUGUI messageText;
    public AudioSource blizzardSound;
    public ForestButtonSpawner forestSpawner;
    public ForestController forestController;

    public void CalmStorm()
    {
        stormSnow.Stop();
        stormFog.Stop();
        normalSnow.Play();
        globalLight.intensity = 1;

        if (clickSound != null)
        {
            clickSound.Play();
        }
        if (blizzardSound != null)
            blizzardSound.Stop();


        if (forestSpawner != null)
            forestSpawner.SpawnAtRandomPoint();


        StartCoroutine(DelayedDoorOpen());

    }

    private IEnumerator DelayedDoorOpen()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "La prochaine zone est disponible";
        }
        yield return new WaitForSeconds(3f);

        door.SetActive(false);
        door2.SetActive(false);

        messageText.gameObject.SetActive(false);
        forestController.StartForestChallenge();
    }
}
